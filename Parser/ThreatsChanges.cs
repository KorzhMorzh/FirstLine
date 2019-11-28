namespace Parser
{
    class ThreatsChanges
    {
        public string[] StringOldThreat { get; }
        public string[] StringNewThreat { get; }

        public ThreatsChanges(Threat oldThreat, Threat newThreat)
        {
            if (oldThreat == null)
            {
                StringNewThreat = newThreat.GetStringArrayProps();
                StringOldThreat = new[] { "", "", "", "", "", "", "", "" };
            }
            else if (newThreat == null)
            {
                StringOldThreat = oldThreat.GetStringArrayProps();
                StringNewThreat = new[] { "", "", "", "", "", "", "", "" };
            }
            else
            {
                StringNewThreat = newThreat.GetStringArrayProps();
                StringOldThreat = oldThreat.GetStringArrayProps();
            }
        }
    }
}