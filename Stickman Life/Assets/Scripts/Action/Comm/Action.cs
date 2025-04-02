using System.Collections;
using System.Collections.Generic;
public abstract class Action
{
    #region Init기능
    public virtual void Init()
    {

    }

    //처음에 DataManager를 부여하는 역할
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
