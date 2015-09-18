using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;


namespace SkinBuddy
{
    class Program
    {

        private static Menu Config;
        private static Slider skinValue;
        private static CheckBox enableValue;
        private static string playerChamp;

        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }



        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            try
            {

                playerChamp = Player.Instance.ChampionName;
                if (playerChamp == null) //make sure do not obtain a null value
                    return;

                Config = MainMenu.AddMenu("SkinBuddy", "SkinBuddy");
                Config.AddGroupLabel("Skin Buddy");
                Config.AddLabel("Change Your Champion's Skin");

                skinValue = Config.Add("Change Skin", new Slider("Change Skin", 0, 0, 15));
                enableValue = Config.Add("enableSkin", new CheckBox("Enable"));


                skinValue.OnValueChange += delegate(ValueBase<int> sender, ValueBase<int>.ValueChangeArgs args2)
                {
                    if (!enableValue.CurrentValue)
                        return;
                    Player.SetSkin(playerChamp, sender.CurrentValue);
                };

                if (enableValue.CurrentValue && skinValue.CurrentValue != 0) // checks if the player changed skin before
                    Player.SetSkin(playerChamp, skinValue.CurrentValue);


            }
            catch (Exception ex)
            {
                Chat.Print("SkinBuddy : Error has been occured : " + ex);
            }
        }

  
    }
}
