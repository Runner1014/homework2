# homework2
**1、游戏对象运动的本质是什么？**

- 游戏对象运动的本质，是游戏对象跟随每一帧的变化，空间上发生变化。这里的空间变化包括了游戏对象的transform组件中的position和rotation两个属性，前者是绝对或者相对位置的改变，后者是所处位置的角度的旋转变化。

**2、请用三种方法以上方法，实现物体的抛物线运动。（如，修改Transform属性，使用向量Vector3的方法…）**

	总体思路是：在UpDate()中实现行为，在每一帧更新物体的位置，使其在水平方向匀速运动，竖直方向匀加速运动。

以下用三种方法实现向右上方抛物的运动

- **方法一**
在每一帧更新物体的position属性
```c#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour {

    public float speed = 10;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position += Vector3.up * Time.deltaTime * speed;
        this.transform.position += Vector3.right * Time.deltaTime * 5;
        speed -= Time.deltaTime * 9.8f;
	}
}
```

 - **方法二**
 与方法一类似，只不过直接同时改变Vector3的x和y（以下仅展示关键代码）
 
 ```c#
	void Update () {
        Vector3 move = new Vector3(Time.deltaTime * 5, Time.deltaTime * speed, 0);
        this.transform.position += move;
        speed -= Time.deltaTime * 9.8f;
    }
 ```
 
 - **方法三**
 与方法二类似，不过利用了transform的Translate()函数
 
```c#
    void Update()
    {
        Vector3 move = new Vector3(Time.deltaTime * 5, Time.deltaTime * speed, 0);
        this.transform.Translate(move);
        speed -= Time.deltaTime * 9.8f;
    }
```

**3、写一个程序，实现一个完整的太阳系， 其他星球围绕太阳的转速必须不一样，且不在一个法平面上**

- **布局**
加入物体，修改position调整位置和大小

![这里写图片描述](http://img.blog.csdn.net/20180401114032725?watermark/2/text/aHR0cDovL2Jsb2cuY3Nkbi5uZXQvUnVubmVyMXN0/font/5a6L5L2T/fontsize/400/fill/I0JBQkFCMA==/dissolve/70/gravity/SouthEast)

- **修改外观**
	向每个星球拖入相应图片材料

- **写行为脚本**
	将除月亮外的所有星球的转动行为都写在一个脚本，并将该脚本作为sun的组件，代码如下：
```c#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour {
    public Transform sun;
    public Transform Mercury;
    public Transform Venus;
    public Transform Earth;
    public Transform Mars;
    public Transform Jupiter;
    public Transform Saturn;
    public Transform Uranus;
    public Transform Neptune;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Mercury.Rotate(Vector3.up * Time.deltaTime * 100);
        Mercury.RotateAround(sun.position, new Vector3(0, 1, 0), 100 * Time.deltaTime);

        Venus.Rotate(Vector3.up * Time.deltaTime * 100);
        Venus.RotateAround(sun.position, new Vector3(0.1f, 1, 0), 90 * Time.deltaTime);

        Earth.Rotate(Vector3.up * Time.deltaTime * 100);
        Earth.RotateAround(sun.position, new Vector3(0.2f, 1, 0), 80 * Time.deltaTime);

        Mars.Rotate(Vector3.up * Time.deltaTime * 100);
        Mars.RotateAround(sun.position, new Vector3(0.3f, 1, 0), 70 * Time.deltaTime);

        Jupiter.Rotate(Vector3.up * Time.deltaTime * 100);
        Jupiter.RotateAround(sun.position, new Vector3(0, 1, 0.1f), 60 * Time.deltaTime);

        Saturn.Rotate(Vector3.up * Time.deltaTime * 100);
        Saturn.RotateAround(sun.position, new Vector3(0, 1, 0.2f), 50 * Time.deltaTime);

        Uranus.Rotate(Vector3.up * Time.deltaTime * 100);
        Uranus.RotateAround(sun.position, new Vector3(0, 1, 0.3f), 40 * Time.deltaTime);

        Neptune.Rotate(Vector3.up * Time.deltaTime * 100);
        Neptune.RotateAround(sun.position, new Vector3(0.1f, 1, 0.1f), 30 * Time.deltaTime);
    }
}
```
注：其中的public属性可以在Unity右侧栏进行赋值，如下

![这里写图片描述](http://img.blog.csdn.net/20180401115532969?watermark/2/text/aHR0cDovL2Jsb2cuY3Nkbi5uZXQvUnVubmVyMXN0/font/5a6L5L2T/fontsize/400/fill/I0JBQkFCMA==/dissolve/70/gravity/SouthEast)

因为地球的公转时月球应作为其子对象，而地球自转时则不然，故因创建一个空对象作为月球的父对象，使其有跟地球一样的位置和公转行为，但没有自转，其脚本代码如下：
```c#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vearth : MonoBehaviour {
    public Transform sun;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.RotateAround(sun.position, new Vector3(0.2f, 1, 0), 80 * Time.deltaTime);
    }
}
```


moon的脚本代码如下：
```c#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour {
    public Transform Vearth;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.RotateAround(Vearth.position, Vector3.up, 500 * Time.deltaTime);
	}
}

```

- **效果图**

![这里写图片描述](http://img.blog.csdn.net/20180401115801437?watermark/2/text/aHR0cDovL2Jsb2cuY3Nkbi5uZXQvUnVubmVyMXN0/font/5a6L5L2T/fontsize/400/fill/I0JBQkFCMA==/dissolve/70/gravity/SouthEast)
