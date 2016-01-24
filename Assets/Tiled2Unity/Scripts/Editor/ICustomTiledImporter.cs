using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

namespace Tiled2Unity
{
    interface ICustomTiledImporter
    {
        // A game object within the prefab has some custom properites assigned through Tiled that are not consumed by Tiled2Unity
        // This callback gives customized importers a chance to react to such properites.
        void HandleCustomProperties(GameObject gameObject, IDictionary<string, string> customProperties);

        // Called just before the prefab is saved to the asset database
        // A last chance opporunity to modify it through script
        void CustomizePrefab(GameObject prefab);
    }
}

[Tiled2Unity.CustomTiledImporter]
class CustomImporterAddComponent : Tiled2Unity.ICustomTiledImporter
{
    public void HandleCustomProperties(UnityEngine.GameObject gameObject,
        IDictionary<string, string> props)
    {
        // Property for adding mob spawners with properties
        if (props.ContainsKey("Spawn"))
        {
            // If it is a destructable, spawn a destructable
            if (props["Spawn"] == "Destructable")
                gameObject.AddComponent<SpawnDestructable>();
            // For backwards compatibility
            else if (props["Spawn"] == "fl1mob")
            {
                gameObject.AddComponent<Spawner>();
                gameObject.GetComponent<Spawner>().MobStr = "random";
                gameObject.GetComponent<Spawner>().Spawn();
            }
            // Else just assume it's a mob and spawn that
            else
            {
                gameObject.AddComponent<Spawner>();
                gameObject.GetComponent<Spawner>().MobStr = props["Spawn"];
                gameObject.GetComponent<Spawner>().Spawn();
            }
        }

        // Property for making tiles cascade tags
        if (props.ContainsKey("ChildTag"))
        {
            foreach (Transform child in gameObject.transform)
            {
                child.tag = props["ChildTag"];
            }
        }

        // Property for exit triggers
        if (props.ContainsKey("Exit"))
        {
            // Set the tag
            gameObject.tag = "Exit";

            // Add the correct exit component
            // NOTE: Had to do it this ugly way because of the way the import from Tiled works
            if (props["Exit"] == "N")
            {
                gameObject.AddComponent<ExitDoorN>();
            }
            else if (props["Exit"] == "E")
            {
                gameObject.AddComponent<ExitDoorE>();
            }
            else if (props["Exit"] == "S")
            {
                gameObject.AddComponent<ExitDoorS>();
            }
            else if (props["Exit"] == "W")
            {
                gameObject.AddComponent<ExitDoorW>();
            }
            
        }

        // Property for room transforms, to move them below all other objects
        if (props.ContainsKey("transformz"))
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, Convert.ToSingle(props["transformz"]));
        }
    }


    public void CustomizePrefab(GameObject prefab)
    {
        // Do nothing
    }
}

// Examples
/*
[Tiled2Unity.CustomTiledImporter]
class CustomImporterAddComponent : Tiled2Unity.ICustomTiledImporter
{
    public void HandleCustomProperties(UnityEngine.GameObject gameObject,
        IDictionary<string, string> props)
    {
        // Simply add a component to our GameObject
        if (props.ContainsKey("AddComp"))
        {
            gameObject.AddComponent(props["AddComp"]);
        }
    }


    public void CustomizePrefab(GameObject prefab)
    {
        // Do nothing
    }
}
*/
