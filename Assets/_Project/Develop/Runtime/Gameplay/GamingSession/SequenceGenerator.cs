using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;
using System;

namespace Assets._Project.Develop.Runtime.Gameplay
{
    public class SequenceGenerator 
    {
        private readonly ConfigsProviderService _configProvider;

        public SequenceGenerator(ConfigsProviderService configProvider)
        {
            _configProvider = configProvider;
        }

        public char[] GenerateSequence(ModeType type)
        {
            QuestTypeConfigTemplate config = _configProvider.GetConfig<QuestTypeConfigTemplate>();

            char[] symbols;

            switch (type)
            {
                case ModeType.Digital:
                    symbols = config.Digitals;
                    break;
                case ModeType.Literal:
                    symbols = config.Literals;
                    break;
                default:
                    throw new ArgumentException($"Неизвестный режим игры: {type}");
            }

            int sequenceLength = config.SequenceLength;
            char[] sequence = new char[sequenceLength];

            for (int i = 0; i < sequenceLength; i++)
                sequence[i] = symbols[UnityEngine.Random.Range(0, symbols.Length)];

            return sequence;
        }
    }
}
