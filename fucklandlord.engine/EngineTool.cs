﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace fucklandlord.engine
{
    public class EngineTool
    {
        /// <summary>
        /// 格式化扑克牌字符串
        /// 将带有花色、无顺序的扑克牌转换成无花色、统一顺序。主要用于牌型检测。
        /// 举例：
        /// 
        /// 3*H-4*S-7*H-6*D-5*S
        /// 格式化后：
        /// 7-6-5-4-3
        /// 
        /// 4*D-4*C-4*H-3*C-3*S-3*H-BJ-A*S
        /// 格式化后：
        /// BJ-A-4-4-4-3-3-3
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static String FormatCardStr(String input)
        {
            String output = null;;
            List<String>  withColors = input.Split('-').ToList();
            List<String> withoutColors = new List<string>();

            withColors.ForEach((s) => 
            {
                if (s.Contains("*"))  // 去掉花色
                {
                    withoutColors.Add(s.Split('*')[0]);
                }
                else
                {
                    withoutColors.Add(s);
                }
            });

            // 按照事先定义的规则 进行降序排序
            withoutColors.Sort((a, b) => 
            {
                int index_a = EngineValues.CardValues.IndexOf(a);
                int index_b = EngineValues.CardValues.IndexOf(b);

                if (index_a > index_b)
                {
                    return -1;
                }
                else
                {
                    return 1;
                }
            });

            output = String.Join("-", withoutColors);

            return output;
        }

        /// <summary>
        /// 计算扑克牌中某个指定卡牌出现的次数，input不带花色
        /// </summary>
        /// <param name="input"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int CountInCardStr(String input, String target)
        {
            int count = 0;
            input.Split('-').ToList().ForEach((a) =>
            {
                if (a.Equals(target))
                {
                    count++;
                }
            });

            return count;
        }

        /// <summary>
        /// 重复指定卡牌N次，中间用-隔开
        /// </summary>
        /// <param name="index"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static String RepeatCard(int index, int count)
        {
            if (count <= 0 || index < 0 || index > 14)
            {
                return null;
            }

            String card_str = "";
            for (int i = 0; i < count; i++)
            {
                card_str += EngineValues.CardValues[index] + "-";
            }

            card_str = card_str.TrimEnd('-');

            return card_str;
        }

        /// <summary>
        /// 负责将类似 {3-3-3, A, 6-6, BJ, 4-4}  整理成  BJ-A-6-6-4-4-3-3-3
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static String SuperSort(List<String> input)
        {
            String a1, b1;
            int index_a, index_b;

            input.Sort((a, b) =>
            {
                a1 = a.Split('-')[0];
                b1 = b.Split('-')[0];

                index_a = EngineValues.CardValues.IndexOf(a1);
                index_b = EngineValues.CardValues.IndexOf(b1);

                if (index_a > index_b)
                {
                    return -1;
                }
                else
                {
                    return 1;
                }
            });

            return String.Join("-", input);
        }

        /// <summary>
        /// 计算指定卡牌的索引（0~53 或者 0~14）
        /// </summary>
        /// <param name="card_str"></param>
        /// <param name="colored"></param>
        /// <returns></returns>
        public static int IndexOfCard(String card_str, bool colored)
        {
            if (colored)  // 有花色
            {
                return EngineValues.Cards.IndexOf(card_str);
            }
            else  // 无花色
            {
                return EngineValues.CardValues.IndexOf(card_str);
            }
        }
    }
}