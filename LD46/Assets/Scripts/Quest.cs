using System.Collections;
using System.Collections.Generic;
using UnityEngine;

   public enum Resource {Money, Notoriety, Cultists, Time }

public class Quest
{


    public Resource res;
    public int amount;
    public bool completed;

    public string questName;
    public string questDescription;
    public Sprite questImage;

    public Quest(Resource _res, int _amount, bool _completed, Sprite _image)
    {
        res = _res;
        amount = _amount;
        completed = _completed;
        questName = GenerateName();
        questDescription = GenerateDescription();
        questImage = _image;
    }

    string GenerateName()
    {
        string newName = "";

        if(res != Resource.Time)
        {
            newName += "Gain ";
            switch (res)
            {
                case Resource.Money:
                    newName += "Money";
                    break;
                case Resource.Notoriety:
                    newName += "Notoriety";

                    break;
                case Resource.Cultists:
                    newName += "Cultists";

                    break;
                
                default:
                    break;
            }
        }
        else
        {
            newName += "Survival";
        }

        return newName;
    }

    string GenerateDescription()
    {
        string newDescription = "";
        if (res != Resource.Time)
        {
            newDescription += "Gain a total of " + amount.ToString();



            switch (res)
            {
                case Resource.Money:
                    newDescription += " Monies";
                    break;
                case Resource.Notoriety:
                    newDescription += " Notoriety";

                    break;
                case Resource.Cultists:
                    newDescription += " Cultists";

                    break;

                default:
                    break;
            }
        }
        else
        {
            newDescription += "Survive for a total of " + amount.ToString() + " seconds.";
        }
        return newDescription;
    }


}
