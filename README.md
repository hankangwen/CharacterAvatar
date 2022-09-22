# CharacterAvatar
CharacterAvatar-Unity换装

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