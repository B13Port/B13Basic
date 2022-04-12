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

        // ��Ⱦ�ű�����
        [ReadOnly] public List<RollListRendererItem> mList_items;
        [LabelText("�߽���ࣨ���ϣ�")] public Vector2 Padding;
        [LabelText("��Ԫ��ߴ磨���ߣ�")] public Vector2 CellSize;
        [LabelText("��Ԫ���϶��ˮƽ����ֱ��")] public Vector2 SpacingSize;
        [LabelText("��������")] public LimitType limitType = LimitType.Horizontal;
        [LabelText("��������")] public int LimitCount;
        [LabelText("��Ԫ����Ⱦ��prefab")] public GameObject RenderGO;

        //λ�õ���Ϣ���������꣩
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

        //��ʼ��λ����Ϣ
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

        //����ָ��Ŀ��
        public void JumpIndex(int index)
        {

        }
    }


}