using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace extOSC.Examples
{
public class IPSaver : MonoBehaviour
{
    public TMP_InputField ipInput;
    public TMP_InputField iPadInput;
    public TextMeshProUGUI ipSetMessage;
    public TextMeshProUGUI iPadAddressStorage;
    public GameObject oscManager;
    public GameObject startButton;

  
     // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void saveIP(){
    	string incomingText = ipInput.text;
        string incomingText2= iPadInput.text;

        OSCTransmitter transmitter = oscManager.GetComponent<OSCTransmitter>();

        transmitter.RemoteHost = incomingText;
        ipSetMessage.text= "IP has been set to " + incomingText + " and this is iPad " + incomingText2;
        ipSetMessage.color= new Color32(190, 230, 180, 255);

        iPadAddressStorage.text=incomingText2;

        startButton.SetActive(true);

    	//Debug.Log(incomingText);
        //Debug.Log("ipAddress:");
        //Debug.Log(transmitter.RemoteHost);

    }
}
}