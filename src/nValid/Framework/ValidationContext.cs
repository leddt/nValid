using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;

namespace nValid.Framework
{
    public class ValidationContext : IValidationContext
    {
        private readonly IDictionary<Type, IList<IRuleSet>> _rscache;
        private readonly IList<IRuleSet> _rulesets;

        private static IValidationContext current;
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
        }

        public static IList<ResourceManager> ResourceManagers { get; private set; }
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
            _rscache = new Dictionary<Type, IList<IRuleSet>>();
            _rulesets = new List<IRuleSet>();
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
            _rulesets.Add(set);
            _rscache.Clear();
        }

        public void AddRuleSet<TInstance>(IList<IRule> rules)
        {
            _rulesets.Add(new RuleSet(typeof(TInstance), rules));
            _rscache.Clear();
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
            if (!_rscache.ContainsKey(type))
            {
                var sets = FindRuleSets(type);
                _rscache.Add(type, sets);
            }

            return _rscache[type];
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
            return (from s in _rulesets
                    where s.ValidatedType == type
                    select s).SingleOrDefault();
        }
    }
}
