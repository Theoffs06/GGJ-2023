using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarScript : MonoBehaviour
{
    [SerializeField] private Slider mainBar;
    [SerializeField] private Slider[] HPCells;
    [SerializeField] private PlayerCharacter player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        mainBar.value = player.HP;
        if(player.Life > 0)
            HPCells[player.Life - 1].value = player.HP;

    }
}
