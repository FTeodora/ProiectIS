using Microsoft.AspNetCore.SignalR;
using ProiectIS.Models;
using ProiectIS.Controllers;
namespace ProiectIS.Hubs
{
    public class QuizLobby:Hub
    {
        public void initializeRoom(string groupName)
        {
            HubManager.Instance.initializeRoom(groupName);
        }
        public async Task JoinRoom(string groupName)
        {
            if (!HubManager.Instance.checkKey(groupName))
                HubManager.Instance.initializeRoom(groupName);
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            HubManager.Instance.addMember(groupName);
            await Clients.Group(groupName).SendAsync("ReceiveMessage", $"{Context.GetHttpContext().Session.GetString("Nume")} ", HubManager.Instance.getMembers(groupName));
            //await Clients.Group(groupName).SendAsync("updateCount", HubManager.Instance.getMembers(groupName));
        }

        public async Task LeaveRoom(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            HubManager.Instance.removeMember(groupName);
            await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has left the group {groupName}.");
        }
        public async Task SendMessage(string user)
        {
            await Clients.All.SendAsync("ReceiveMessage");
        }
        
        
    }
    public class LobbyManager
    {
        public int members { get; set; }
        public List<Question> questions { get; set; }
        public LobbyManager()
        {
            members = 0;
            questions = new List<Question>();
        }
      
    }
    public class HubManager
    {
        private static HubManager instance = null;
        private Dictionary<string, LobbyManager> roomList;
        private HubManager()
        {
            //Console.WriteLine("A fost null");
            roomList = new Dictionary<string, LobbyManager>();
        }
        public static HubManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new HubManager();
                return instance;
            }
        }
        public void addMember(string room)
        {
            roomList[room].members++;
        }
        public void removeMember(string room)
        {
            roomList[room].members--;
        }
        public void initializeRoom(string room)
        {
            roomList[room] = new LobbyManager();
        }
        public long getMembers(string room)
        {
            return roomList[room].members;
        }
        public bool checkKey(string room)
        {
            return roomList.ContainsKey(room);
        }
        public void makeQuestions(long room)
        {
            roomList[room.ToString()].questions = QuizController.generateQuestions(6);
        }
        public List<Question> getLobbyQuestions(long room)
        {
            return roomList[room.ToString()].questions;
        }
    }
}
