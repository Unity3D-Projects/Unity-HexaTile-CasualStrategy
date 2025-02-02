﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TilePuzzle.Procedural
{
    /// <summary>
    /// <see cref="BiomeTableSettings"/>의 설정값을 기반으로 만들어지는 바이옴 테이블
    /// </summary>
    public class BiomeTable
    {
        /// <summary>
        /// 습도 구간 개수
        /// </summary>
        private const int MoistureLevels = BiomeTableSettings.MoistureLevels;
        /// <summary>
        /// 온도 구간 개수
        /// </summary>
        private const int TemperatureLevels = BiomeTableSettings.TemperatureLevels;

        public readonly IReadOnlyDictionary<int, Biome> biomeDictionary;
        private readonly Biome[,] biomeMap;

        public BiomeTable(BiomeTableSettings.BiomeData mainBiomeData, IEnumerable<BiomeTableSettings.BiomeData> subBiomeDatas)
        {
            biomeMap = new Biome[MoistureLevels, TemperatureLevels];

            var newBiomeDictionary = new Dictionary<int, Biome>();

            // 메인 바이옴으로 바이옴 맵 초기화
            Biome mainBiome = new Biome(mainBiomeData.biomeName, mainBiomeData.color, mainBiomeData.tags);
            newBiomeDictionary.Add(mainBiome.id, mainBiome);
            for (int y = 0; y < TemperatureLevels; y++)
            {
                for (int x = 0; x < MoistureLevels; x++)
                {
                    biomeMap[x, y] = mainBiome;
                }
            }

            // 서브 바이옴들을 바이옴 맵에 할당
            foreach (var subBiomeData in subBiomeDatas)
            {
                Biome subBiome = new Biome(subBiomeData.biomeName, subBiomeData.color, subBiomeData.tags);
                newBiomeDictionary.Add(subBiome.id, subBiome);
                for (int y = subBiomeData.temperatureRange.x; y < subBiomeData.temperatureRange.y; y++)
                {
                    for (int x = subBiomeData.moistureRange.x; x < subBiomeData.moistureRange.y; x++)
                    {
                        biomeMap[x, y] = subBiome;
                    }
                }
            }

            biomeDictionary = newBiomeDictionary;
        }

        /// <summary>
        /// <paramref name="moisture"/>와 <paramref name="temperature"/>값에 대응하는 바이옴을 반환
        /// </summary>
        /// <param name="moisture">습도 0 ~ 1</param>
        /// <param name="temperature">온도 0 ~ 1</param>
        public Biome EvaluateBiome(float moisture, float temperature)
        {
            int x = Mathf.Clamp(Mathf.FloorToInt(moisture * MoistureLevels), 0, MoistureLevels - 1);
            int y = Mathf.Clamp(Mathf.FloorToInt(temperature * TemperatureLevels), 0, TemperatureLevels - 1);
            return biomeMap[x, y];
        }
    }
}
