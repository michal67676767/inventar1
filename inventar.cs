using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class TableManager : MonoBehaviour
{
    //sem si v unity pretiahneme nase input fieldy
    public TMP_InputField nameimputfield;
    public TMP_InputField valueinputfield;

    //sem pretiahneme contenty z tabuliek
    public Transform content1;
    public Transform content2;

    //prefab tlacitka co sa bude kopirovat
    public GameObject buttonPrefab;

    //si spravime obycajnu triedu pre data jednej polozky, aby sme mali vsetko pokope
    public class PolozkaData
    {
        public int cisloHodnota;
        public GameObject tlacitko1;
        public GameObject tlacitko2;
        public TMP_Text textNaDruhomTlacitku;
    }

    //slovnik, kde budeme drzat meno polozky a k nemu jej data
    private Dictionary<string, PolozkaData> zoznamVsetkeho = new Dictionary<string, PolozkaData>();

    //tato funkcia sa stane ked kliknes na add
    public void OnAddButtonPressed()
    {
        //precitame texty z inputov
        string meno = nameimputfield.text;
        string textCislo = valueinputfield.text;

        //ked je daco prazdne, tak koncime
        if (meno == "" || textCislo == "")
        {
            return;
        }

        //skusime ci to druhe je normalne cislo
        int cislo;
        bool jeToCislo = int.TryParse(textCislo, out cislo);

        if (jeToCislo == true)
        {
            //pozrieme ci uz taky nazov mame
            if (zoznamVsetkeho.ContainsKey(meno))
            {
                //ak mame, tak iba priratame cislo
                PolozkaData data = zoznamVsetkeho[meno];
                data.cisloHodnota = data.cisloHodnota + cislo;

                //prepiseme text na druhom tlacitku
                data.textNaDruhomTlacitku.text = data.cisloHodnota.ToString();
            }
            else
            {
                //ak nemame, tak vytvorime nove tlacitko v prvej tabulke pre meno
                GameObject novyObj1 = Instantiate(buttonPrefab, content1);
                TMP_Text t1 = novyObj1.GetComponentInChildren<TMP_Text>();
                t1.text = meno;

                //vytvorime tlacitko v druhej tabulke pre cislo
                GameObject novyObj2 = Instantiate(buttonPrefab, content2);
                TMP_Text t2 = novyObj2.GetComponentInChildren<TMP_Text>();
                t2.text = cislo.ToString();

                //vytvorime novu skatulku s datami
                PolozkaData novaData = new PolozkaData();
                novaData.cisloHodnota = cislo;
                novaData.tlacitko1 = novyObj1;
                novaData.tlacitko2 = novyObj2;
                novaData.textNaDruhomTlacitku = t2;

                //hodime to do slovnika
                zoznamVsetkeho.Add(meno, novaData);
            }

            //vycistime policka
            nameimputfield.text = "";
            valueinputfield.text = "";
        }
    }

    //tato funkcia sa stane ked kliknes na remove
    public void OnRemoveButtonPressed()
    {
        string meno = nameimputfield.text;
        string textCislo = valueinputfield.text;

        if (meno == "" || textCislo == "")
        {
            return;
        }

        int minusCislo;
        bool jeToCislo = int.TryParse(textCislo, out minusCislo);

        if (jeToCislo == true)
        {
            //pozrieme ci to meno vobec existuje
            if (zoznamVsetkeho.ContainsKey(meno))
            {
                PolozkaData data = zoznamVsetkeho[meno];

                //odratame cislo
                data.cisloHodnota = data.cisloHodnota - minusCislo;

                //ak je to menej alebo rovne nule
                if (data.cisloHodnota <= 0)
                {
                    //zmazeme oba gameobjecty z hry
                    Destroy(data.tlacitko1);
                    Destroy(data.tlacitko2);

                    //vyhodime zo slovnika
                    zoznamVsetkeho.Remove(meno);
                }
                else
                {
                    //inak len aktualizujeme text cisla
                    data.textNaDruhomTlacitku.text = data.cisloHodnota.ToString();
                }
            }

            //vycistime policka
            nameimputfield.text = "";
            valueinputfield.text = "";
        }
    }
}