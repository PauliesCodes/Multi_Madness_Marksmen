using UnityEngine;
using Discord;
public class DicsordControler : MonoBehaviour
{

    public Discord.Discord discord;

    // Start is called before the first frame update
    void Start()
    {
        discord = new Discord.Discord(1185900629030346812, (System.UInt64)Discord.CreateFlags.Default);
        var activityManager = discord.GetActivityManager();
        var activity = new Discord.Activity {

            Details = "Playing solo",
            State = "Trying to get the best score!",
            Assets =
            {
                LargeImage = "icon"
            }
        };

        activityManager.UpdateActivity(activity, (res) => {

            if(res == Discord.Result.Ok){

                Debug.Log("Discord status set!");

            }
            else{

                Debug.Log("Neco se podsralo");

            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        
        discord.RunCallbacks();

    }
}
