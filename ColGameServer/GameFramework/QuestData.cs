using System.Collections.Generic;

namespace ColGameServer.GameFramework
{
    public class FullQuestsData
    {
        public List<QuestData> Quests { get; set; }
        public List<RequireNpcInQuest> RequireNpcs { get; set; }
        public List<RequireMonsterInQuest> RequireMonsters { get; set; }
        public List<ReceiveItemInQuest> ReceiveItems { get; set; }
    }
    public class QuestData
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
    }
    public class RequireNpcInQuest
    {
        public string QuestName { get; set; }
        public int NpcID { get; set; }
        public string Conversation { get; set; }
    }
    public class RequireMonsterInQuest
    {
        public string QuestName { get; set; }
        public int MonsterID { get; set; }
        public int Quatity { get; set; }
    }
    public class ReceiveItemInQuest
    {
        public string QuestName { get; set; }
        public int ItemID { get; set; }
        public int Quatity { get; set; }
    }
}
