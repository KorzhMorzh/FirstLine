namespace Parser
{
    class Threat : IThreatable
    {
        public string FullId { get; }
        public int Id { get; }
        public string Name { get; }
        public string Description { get; }
        public string Source { get; }
        public string ThreatSubject { get; }
        public string BreachOfConfidentiality { get; }
        public string BreachOfIntegrity { get; }
        public string BreachOfAvailability { get; }

        public Threat(string[] threat)
        {
            FullId = $"УБИ.{int.Parse(threat[0]):d3}";
            Id = int.Parse(threat[0]);
            Name = threat[1].Replace("_x000d_", "");
            Description = threat[2].Replace("_x000d_", "");
            Source = threat[3].Replace("_x000d_", "");
            ThreatSubject = threat[4].Replace("_x000d_", "");
            BreachOfConfidentiality = threat[5] == "1" ? "да" : "нет";
            BreachOfIntegrity = threat[6] == "1" ? "да" : "нет";
            BreachOfAvailability = threat[7] == "1" ? "да" : "нет";
        }

        public override bool Equals(object obj)
        {
            if (obj is Threat)
            {
                var newThreat = (Threat)obj;
                return Name.Replace("\n", "").Replace("\r", "") == newThreat.Name.Replace("\n", "").Replace("\r", "") &&
                       Source.Replace("\n", "").Replace("\r", "") ==
                       newThreat.Source.Replace("\n", "").Replace("\r", "") &&
                       Description.Replace("\n", "").Replace("\r", "") ==
                       newThreat.Description.Replace("\n", "").Replace("\r", "")
                       && ThreatSubject.Replace("\n", "").Replace("\r", "") ==
                       newThreat.ThreatSubject.Replace("\n", "").Replace("\r", "") &&
                       BreachOfConfidentiality == newThreat.BreachOfConfidentiality &&
                       BreachOfAvailability == newThreat.BreachOfAvailability &&
                       BreachOfIntegrity == newThreat.BreachOfIntegrity;
            }

            return false;
        }

        public string[] GetStringArrayProps()
        {
            return new[]
            {
                FullId, Name, Description, Source,
                ThreatSubject,
                BreachOfConfidentiality, BreachOfIntegrity,
                BreachOfAvailability
            };
        }
    }
}