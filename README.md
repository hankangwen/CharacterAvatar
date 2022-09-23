# CharacterAvatar
CharacterAvatar-Unity角色换装实现

### 介绍
提供了加载Model预制到场景中作为数据备份，

以及提供了把Model信息拆分为txt、mesh、material，加载到Target骨架上的功能。
![image](https://user-images.githubusercontent.com/22899493/191877416-a9e133fa-7183-4041-a6b9-f85c5f79ea28.png)

![image](https://user-images.githubusercontent.com/22899493/191877308-7604a3bd-ea01-42c5-9232-80b2a48ca968.png)

GenerateSkinDataEditor是拆分工具，工具用法见下方：
### 工具用法
GenerateSkinDataEditor 会生成mesh和txt。

其中txt存储格式如下：

```
第1行存储material数量，设为n
第2行开始存储material的名字
第n+2行存储mesh的名字
第n+3行存储rootBon
第n+4行开始都是bones
```
示例：
```
1（材质球数量）
female_pants-1_green（材质球名称）
pants-1（mesh名字）
Female_Hips（RootBone, 这行之后为BoneList）
Female_Hips/Female_RightUpLeg
Female_Hips
Female_Hips/Female_RightUpLeg/Female_RightLeg
Female_Hips/Female_RightUpLeg/Female_RightLeg/Female_RightFoot
Female_Hips/Female_LeftUpLeg
Female_Hips/Female_LeftUpLeg/Female_LeftLeg
Female_Hips/Female_LeftUpLeg/Female_LeftLeg/Female_LeftFoot
Female_Hips/Female_Spine
```




### 链接
https://github.com/getker/CharacterAvatar

https://www.sikiedu.com/course/130
