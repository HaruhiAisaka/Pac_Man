using UnityEngine;

public class EdibleItemsOnBoard : MonoBehaviour
{
    public Pellet[] pellets;

    public SuperPellet[] superPellets;

    public Fruit fruit;

    public bool presetEdibleItems;

    void Start()
    {
        if (presetEdibleItems)
        {
            SetListPresetEdibleItems();
        }
    }

    public void Reset()
    {
        foreach (Pellet pellet in pellets)
        {
            pellet.Reset();
        }

        foreach (SuperPellet superPellet in superPellets)
        {
            superPellet.Reset();
        }
    }
    
    void SetListPresetEdibleItems()
    {
        pellets = this.GetComponentsInChildren<Pellet>();
        superPellets = this.GetComponentsInChildren<SuperPellet>();
    }

    public bool AllPelletsEaten()
    {
        bool allPelletsEaten = true;
        foreach (Pellet pellet in pellets)
        {
            allPelletsEaten = allPelletsEaten && pellet.hasBeenEaten;
        }
        return allPelletsEaten;
    }
}