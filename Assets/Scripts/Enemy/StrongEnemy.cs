using UnityEngine;

// Güçlü düşman. Örneğin oyuncuya yaklaşınca hızı aniden artsın (Dash) diyebiliriz.
// Şimdilik sadece miras alıyor, inspector'dan Can 50, Hız 2, Altın 15 yapacağız.
public class StrongEnemy : BaseEnemy
{
    protected override void Start()
    {
        base.Start(); // BaseEnemy'nin Start içindeki (Rigidbody ve Player bulma) işlemlerini çalıştır.
        // Güçlü düşmana özel ekstra başlangıç işlemleri varsa buraya yazılır.
    }

    // İstenirse MoveTowardsPlayer fonksiyonu 'override' edilip özel bir yürüme algoritması yazılabilir.
}