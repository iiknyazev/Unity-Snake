using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckNewRecords : MonoBehaviour
{
    [SerializeField] private Text _record1;
    [SerializeField] private Text _record2;
    [SerializeField] private Text _record3;
    [SerializeField] private Text _record4;
    [SerializeField] private Text _record5;
    [SerializeField] private Text _record6;
    [SerializeField] private Text _record7;
    [SerializeField] private Text _record8;

    void Start()
    {
        if(EndGame.Records[0] != 0)
            _record1.text = "1. " + EndGame.Records[0].ToString();
        if (EndGame.Records[1] != 0)
            _record2.text = "2. " + EndGame.Records[1].ToString();
        if (EndGame.Records[2] != 0)
            _record3.text = "3. " + EndGame.Records[2].ToString();
        if (EndGame.Records[3] != 0)
            _record4.text = "4. " + EndGame.Records[3].ToString();
        if (EndGame.Records[4] != 0)
            _record5.text = "5. " + EndGame.Records[4].ToString();
        if (EndGame.Records[5] != 0)
            _record6.text = "6. " + EndGame.Records[5].ToString();
        if (EndGame.Records[6] != 0)
            _record7.text = "7. " + EndGame.Records[6].ToString();
        if (EndGame.Records[7] != 0)
            _record8.text = "8. " + EndGame.Records[7].ToString();
    }
}
