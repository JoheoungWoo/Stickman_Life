using System.Collections;
using System.Collections.Generic;
public abstract class Action
{
    #region Init���
    public virtual void Init()
    {

    }

    //ó���� DataManager�� �ο��ϴ� ����
    public virtual void Init(MainData mainData)
    {
    }
    #endregion
    public virtual void Play()
    {

    }

    public virtual void Play(string type)
    {

    }


    public virtual bool PlayBool()
    {
        return default;
    }
    public virtual bool PlayBool(string type)
    {
        return default;
    }
}
