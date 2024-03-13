using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    [SerializeField] private TerrainManager terrainManager => TerrainManager.Instance;
    [SerializeField] private AudioSource source;
    //[SerializeField] private List<TagMatPair> tagMatPairs = new List<TagMatPair>();
    private AudioManager audioManager => AudioManager.Instance;
    public void PlayFootStep()
    {
        audioManager.PlayRandomFromArray(source, GetFootMatAudioPack()?.stepClips);
    }
    public void PlayJump()
    {
        audioManager.PlayAudioOnSource(source, GetFootMatAudioPack()?.jumpClip);
    }
    public void PlayLand()
    {
        audioManager.PlayAudioOnSource(source, GetFootMatAudioPack()?.landClip);
    }
    public FootMatAudioPack GetFootMatAudioPack()
    {
        if (audioManager.footMatAudioPacks.Exists(f => f.mat == GetWalkableMaterial()))
            return audioManager.footMatAudioPacks.First(f => f.mat == GetWalkableMaterial());
        return null;
    }
    public bool OnTerrain(ref RaycastHit hit)
    {
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.5f, ~LayerMask.GetMask("Ignore Raycast", "Ignore Player", "Player", "Enemy")))
        {
            if (terrainManager != null && hit.transform.TryGetComponent(out Terrain terrain))
                return true;
        }
        return false;
    }
    public WalkableMaterial GetWalkableMaterial()
    {
        RaycastHit hit = new RaycastHit();
        bool onTerrain = OnTerrain(ref hit);

        if (onTerrain)
        {
            switch (terrainManager.GetTerrainAtPosition(transform.position))
            {
                case 0:
                    return WalkableMaterial.Grass;
                case 1:
                    return WalkableMaterial.Stone;
                case 2:
                    return WalkableMaterial.Dirt;
                default:
                    return WalkableMaterial.Null;
            }
        }
        else if(hit.transform != null)
        {
            if (System.Enum.TryParse(hit.transform.tag, out WalkableMaterial result))
                return result;
        }
        return WalkableMaterial.Null;
    }
}
public enum WalkableMaterial { Null, Wood, Grass, Stone, Dirt, Liquid, Carpet }

[System.Serializable]
public class TagMatPair
{
    public string tag;
    public WalkableMaterial mat;
}