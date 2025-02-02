﻿using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TilePuzzle.Procedural
{
    public class TerrainGenerateSettings : ScriptableObject
    {
        [Title("General")]
        [Min(10)]
        public Vector2Int terrainSize = new Vector2Int(20, 20);

        [Title("Island Shape")]
        [ProgressBar(0, 1, 0.4f, 0.9f, 0.2f)]
        public float landRatio = 0.6f;
        [Range(0.1f, 1f)]
        public float lakeThreshold = 0.3f;
        [Range(0f, 2f)]
        public float peakMultiplier = 1.1f;
        [FoldoutGroup("Island Shape Noise", GroupName = "Noise Settings"), HideLabel]
        public NoiseSettings terrainShapeNoiseSettings;
        [FoldoutGroup("Island Shape Falloff", GroupName = "Falloff Settings"), HideLabel]
        public FalloffSettings terrainShapeFalloffSettings;

        [Title("River")]
        public int riverSeed;
        [MinMaxSlider(0, 1, true)]
        public Vector2 riverSpawnRange = new Vector2(0.3f, 0.9f);
        public float riverSpawnMultiplier = 1;

        [Title("Biome")]
        [Required]
        public BiomeTableSettings biomeTableSettings;
        [Range(0, 1)]
        public float riverMoistureFactor = 0.2f;
        public bool isSeaProvideMoisture = false;
        [FoldoutGroup("Biome Noise", GroupName = "Noise Settings"), HideLabel]
        public NoiseSettings biomeNoiseSettings;
    }
}
