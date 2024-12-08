using Assets.Scripts;
using Assets.Scripts.ServerRequests;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Testing();
        }
    }

    public async void Testing()
    {
        OnDataGot(await ServerRequest.GetAllLogs());
    }

    private void OnDataGot(ShopLog[] obj)
    {
        Debug.Log("got data");
        foreach (var item in obj)
        {
            var a = $"{item.comment}\n{item.player_name}\n{item.shop_name}\n";
            foreach (var b in item.resources_changed)
            {
                a += $"{b.Key} = {b.Value}\n";
            }
            Debug.Log(a);
        }
        Debug.Log("End of data");
    }
}
