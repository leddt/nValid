﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;

namespace nValid.Framework
{
    public class ValidationContext : IValidationContext
    {
        private readonly IDictionary<Type, IList<IRuleSet>> rscache;
        private readonly IList<IRuleSet> rulesets;

        private static IDictionary<string, IValidationContext> namedContexts;
        private static IValidationContext current;

        public static IList<ResourceManager> ResourceManagers { get; private set; }

        public static IValidationContext Current
        {
            get
            {
                if (current == null)
                    current = new ValidationContext();

                return current;
            }

            set { current = value; }
        }

        /// <summary>
        /// Resets all validation configuration.
        /// </summary>
        public static void Reset()
        {
            Current = null;
            ResourceManagers = new List<ResourceManager>();
            namedContexts = new Dictionary<string, IValidationContext>();
        }

        public static IValidationContext GetNamedContext(string contextName)
        {
            var name = contextName.ToLowerInvariant();

            if (!namedContexts.ContainsKey(name))
            {
                lock (namedContexts)
                {
                    if (!namedContexts.ContainsKey(name))
                        namedContexts.Add(name, new ValidationContext());
                }
            }

            return namedContexts[name];
        }

        internal static void SetNamedContext(string contextName, IValidationContext context)
        {
            var name = contextName.ToLowerInvariant();

            lock (namedContexts)
            {
                if (namedContexts.ContainsKey(name))
                    namedContexts[name] = context;
                else
                    namedContexts.Add(name, context);
            }
        }

        private static ResourceManager DefaultResourceManager
        {
            get
            {
                return new ResourceManager(
                    "nValid.Resources.DefaultMessages", 
                    typeof (ValidationContext).Assembly
                );
            }
        }

        protected ValidationContext()
        {
            rscache = new Dictionary<Type, IList<IRuleSet>>();
            rulesets = new List<IRuleSet>();
        }

        static ValidationContext()
        {
            Reset();
        }

        public ValidationResult Validate(object instance)
        {
            var brokenRules = new List<BrokenRule>();
            var sets = GetRulesetsFor(instance.GetType());

            foreach (var set in sets)
            {
                var r = set.Validate(instance);
                brokenRules.AddRange(r.BrokenRules);

                if (r.StopEvaluation)
                    break;
            }

            return new ValidationResult(brokenRules);
        }

        public void AddRuleSet(IRuleSet set)
        {
            rulesets.Add(set);
            rscache.Clear();
        }

        public void AddRuleSet<TInstance>(IList<IRule> rules)
        {
            rulesets.Add(new RuleSet(typeof(TInstance), rules));
            rscache.Clear();
        }

        public static string GetResourceString(string key)
        {
            string value = null;

            foreach (var rm in ResourceManagers)
            {
                value = rm.GetString(key);

                if (!String.IsNullOrEmpty(value))
                    break;
            }

            if (String.IsNullOrEmpty(value))
                value = DefaultResourceManager.GetString(key);
            
            return value;
        }

        protected IList<IRuleSet> GetRulesetsFor(Type type)
        {
            if (!rscache.ContainsKey(type))
            {
                var sets = FindRuleSets(type);
                rscache.Add(type, sets);
            }

            return rscache[type];
        }

        private List<IRuleSet> FindRuleSets(Type type)
        {
            var sets = new List<IRuleSet>();

            if (type.BaseType != null)
                sets.AddRange(GetRulesetsFor(type.BaseType));

            var set = FindExactRuleSet(type);
            var iindex = 0;

            if (set != null && !sets.Contains(set))
            {
                sets.Insert(0, set);
                iindex = 1;
            }

            foreach (var iface in type.GetInterfaces().Reverse())
            {
                set = FindExactRuleSet(iface);

                if (set != null && !sets.Contains(set))
                    sets.Insert(iindex, set);
            }

            return sets;
        }

        private IRuleSet FindExactRuleSet(Type type)
        {
            return (from s in rulesets
                    where s.ValidatedType == type
                    select s).SingleOrDefault();
        }
    }
}
