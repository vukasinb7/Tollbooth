using System.Text.Json.Serialization;

namespace TollStationSystem.Core.Users.Model
{
    public class Boss : User
    {
        int tollStationId;

        public Boss(string jmbg, string name, string lastName, string phone, string mail, Adress adress,
            UserType userType, Account account, int tollStationId) :
            base(jmbg, name, lastName, phone, mail, adress, userType, account)
        {
            this.tollStationId = tollStationId;
        }

        [JsonPropertyName("tollStationId")]
        public int TollStationId { get => tollStationId; set => tollStationId = value; }
    }

}
