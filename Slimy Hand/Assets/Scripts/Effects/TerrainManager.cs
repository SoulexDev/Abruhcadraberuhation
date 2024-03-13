using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    public static TerrainManager Instance;

    public TerrainData mTerrainData;
    public Terrain currentTerrain;
    public int alphamapWidth;
    public int alphamapHeight;

    public float[,,] mSplatmapData;
    public int mNumTextures;

    private void Awake()
    {
        Instance = this;
        GetTerrainProps();
    }
    public void GetTerrainProps()
    {
        if (currentTerrain == null)
        {
            Instance = null;
            return;
        }
        mTerrainData = currentTerrain.terrainData;

        //Debug.Log(mTerrainData);
        alphamapWidth = mTerrainData.alphamapWidth;
        alphamapHeight = mTerrainData.alphamapHeight;

        mSplatmapData = mTerrainData.GetAlphamaps(0, 0, alphamapWidth, alphamapHeight);
        mNumTextures = mSplatmapData.Length / (alphamapWidth * alphamapHeight);
    }

    private Vector3 ConvertToSplatMapCoordinate(Vector3 playerPos)
    {
        Vector3 vecRet = new Vector3();
        Terrain ter = currentTerrain;
        Vector3 terPosition = ter.transform.position;
        vecRet.x = ((playerPos.x - terPosition.x) / ter.terrainData.size.x) * ter.terrainData.alphamapWidth;
        vecRet.z = ((playerPos.z - terPosition.z) / ter.terrainData.size.z) * ter.terrainData.alphamapHeight;
        return vecRet;
    }

    private int GetActiveTerrainTextureIdx(Vector3 pos)
    {
        Vector3 TerrainCord = ConvertToSplatMapCoordinate(pos);
        int ret = 0;
        float comp = 0f;
        for (int i = 0; i < mNumTextures; i++)
        {
            //Debug.Log(i);
            if (comp < mSplatmapData[(int)TerrainCord.z, (int)TerrainCord.x, i])
                ret = i;
        }
        return ret;
    }

    public int GetTerrainAtPosition(Vector3 pos)
    {
        int terrainIdx = GetActiveTerrainTextureIdx(pos);
        return terrainIdx;
    }
}