using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DataLoader : MonoBehaviour
{
    [SerializeField] private Animator _Ui;
    [SerializeField] private RacketLayoutController _LayoutController;
    public List<string> colors;

    private bool _DataLoaded_Colors;

    private void Awake()
    {
        _Ui = GameObject.Find("Canvas - UI").GetComponent<Animator>();
        _LayoutController = GameObject.Find("Layout Controller").GetComponent<RacketLayoutController>();
        StartCoroutine(GetRequest("//localhost/dahcor_backend/GetColorData.php"));
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                    _DataLoaded_Colors = false;
                    break;
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    _DataLoaded_Colors = false;
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    _DataLoaded_Colors = false;
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log("<color=green>" + pages[page] + " Successfull:</color>" + "\nReceived: " + webRequest.downloadHandler.text);

                    var split = webRequest.downloadHandler.text.Split('.');

                    foreach (var item in split)
                    {
                        if (item.Length == 9)
                            colors.Add(item);
                    }

                    _DataLoaded_Colors = true;

                    CheckIfAllDataIsLoaded();

                    break;
            }
        }
    }

    private void CheckIfAllDataIsLoaded()
    {
        if (!_DataLoaded_Colors)
            return;


        // All data is Loaded
        _Ui.SetTrigger("StartFadeIn");

        _LayoutController.UpdateQuestionsWithNewData();
    }
}