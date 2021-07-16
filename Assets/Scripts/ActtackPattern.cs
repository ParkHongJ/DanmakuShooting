using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActtackPattern : MonoBehaviour
{
    enum AttackType
    {
        Ground = 1, //땅인지
        Sky //하늘인지
    }
    enum RangeType
    {
        Direction = 1, //방향
        Range //범위
    }
    enum AttackShape
    {   
        Projectile = 1, //투사체
        Summons, //소환형
        Channeling //채널링
    }
    enum HitType
    {
        Single = 1, //단일
        Multi //범위
    }
    public string name; //이름
    public string variableName; //변수명
    public float objectRange; //객체거리
    public float cooltime; //쿨타임
    public float height; //높이
    public float range; //사정거리
    public float damage; //데미지

    AttackType attackType;
    RangeType rangeType;
    HitType hitType;
    AttackShape attackShape;


    public virtual void Attack()
    {

    }
}
