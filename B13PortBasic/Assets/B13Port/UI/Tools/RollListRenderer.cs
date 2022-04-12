using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace B13Port.Common
{

    public class RollListRenderer : MonoBehaviour
    {
        protected IList mDataProviders;
        public enum LimitType
        {
            Horizontal = 0,
            Vertical = 1
        }

        // 渲染脚本集合
        [ReadOnly] public List<RollListRendererItem> mList_items;
        [LabelText("边界空余（左，上）")] public Vector2 Padding;
        [LabelText("单元格尺寸（宽，高）")] public Vector2 CellSize;
        [LabelText("单元格间隙（水平，垂直）")] public Vector2 SpacingSize;
        [LabelText("限制类型")] public LimitType limitType = LimitType.Horizontal;
        [LabelText("限制行数")] public int LimitCount;
        [LabelText("单元格渲染器prefab")] public GameObject RenderGO;

        //位置点信息（本地坐标）
        private Dictionary<int, Vector2> posDic = new Dictionary<int, Vector2>();

        public virtual void InitRendererList(IList datas)
        {
            mDataProviders = datas;
            InitPosinfo();
            for (int i = 0; i < LimitCount; i++)
            {
                GameObject child = Instantiate(RenderGO);
                child.transform.SetParent(transform);
                child.transform.localRotation = Quaternion.identity;
                child.transform.localScale = Vector3.one;
                child.layer = gameObject.layer;
                child.SetActive(true);
                child.transform.localPosition = posDic[i];

                RollListRendererItem dfItem = child.GetComponent<RollListRendererItem>();
                if (dfItem == null)
                    throw new Exception("Render must extend DynamicInfinityItem");
                dfItem.SetData(datas[i]);
                mList_items.Add(dfItem);
            }
            UpdateRender();
        }


        public void UpdateRender()
        {
            for (int i = 0; i < mList_items.Count; i++)
            {
                mList_items[i].OnRenderer();
            }
        }


        public IList GetDataProvider() { return mDataProviders; }

        //初始化位置信息
        private void InitPosinfo()
        {
            if (limitType == LimitType.Horizontal)
            {
                for (int i = 0; i < LimitCount; i++)
                {
                    float x = Padding.x;
                    x += ((CellSize.x + SpacingSize.x) * i);
                    Vector2 temp = new Vector2(x, -Padding.y);
                    posDic.Add(i, temp);
                }

            }
            else if (limitType == LimitType.Vertical)
            {
                for (int i = 0; i < LimitCount; i++)
                {
                    float y = -Padding.y;
                    y -= ((CellSize.y + SpacingSize.y) * i);
                    Vector2 temp = new Vector2(Padding.x, y);
                    posDic.Add(i, temp);
                }
            }


        }

        //跳到指定目标
        public void JumpIndex(int index)
        {

        }
    }


}