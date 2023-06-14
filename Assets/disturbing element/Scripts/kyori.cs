using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class kyori : MonoBehaviour
{
    [SerializeField] private GameObject partner;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject mae;
    [SerializeField] private GameObject usiro;
    private float playerpos;
    private float partnerpos;

    [SerializeField] private Text _kyori;
    void Start()
    {
        _kyori.text = "";
    }

    
    void Update()
    {
        float dis = Vector2.Distance(new Vector2(player.transform.position.x,0), new Vector2(partner.transform.position.x,0));
        

        _kyori.text = string.Format("“G‚Æ‚Ì‹——£@ @{0}",dis.ToString("n1"));


        Vector2 playerpos = player.transform.position;
        Vector2 partnerpos = partner.transform.position;

        Vector2 v1 = playerpos - partnerpos;
        Vector2 v2 = new Vector2(0.0f,0.0f);

        if(player.transform.position.x>partner.transform.position.x)
        {
            mae.SetActive(false);
            usiro.SetActive(true);
        }
        else
        {
            mae.SetActive(true);
            usiro.SetActive(false);
        }
        

        
    }
}
