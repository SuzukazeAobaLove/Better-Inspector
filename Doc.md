# Ŀ¼
[1.����](#Variable)</br>
[2.��Դ�ļ�](#ResourceAsset)</br>
[3.����](#Attribute)</br>

## ����<a name= "Variable"></a>

### ��ʱ������
DecUInt��һ��ʵ����ILifeCycle�ı��������Ƕ�UInt��һ���װ������ÿһ������֡�Լ���
ʹ�ù��캯����ʼ����ʱ����Ҫһ��GameObject������Ϊ���صĶ���
һ���ö������٣�DecUIntҲ�����١�
</br>
ʹ������
<ul>
<li> ```DecUInt foo = new(gameObject);``` </li>

</ul>

</br></br>
DelegateUInt��DecUInt�Ļ����ϼ����˼�ʱ���㴥���ص��Ĺ��ܣ���ʹ�ù��캯����ʼ����ʱ��ָ���ص���
ͬʱ��DelegateUIntһ����ʱ��Ϊ�㣬���޷��ٴ�����ʱ�䣬ֻ��ͨ��Trigger����������Cancelȡ��������
Lengthen�ӳ��������и��ġ�
</br></br>

## ��Դ�ļ�<a name = "ResourceAsset"></a>

### ����������Դ
MultiAttributeResource��һ���̳���ScriptObject����Դ�ļ��������԰������ɸ�String��Int��Bool��Float������
�����й����У�ֻ��Ҫ����MAR����ʹ��GetString�������ܻ�ȡָ��������ֵ��
</br>
��Inspector��ֻ�ܲ鿴�����������ԣ������Ҫ�༭������MikanLab/����������Դ�༭�������н��С�
</br></br>

### �����ʩ���У�
</br></br>


## ����<a name = "Attribute"></a>

### ��̬��������
����[TrackStatic]���ཫ���뾲̬���������б����еľ�̬���������KikanLab/��̬�����������в鿴��
</br>
���ھ�̬�ֶκ;�̬���ԣ�����[EditableStatic]����ʾ�ɱ༭����ֵ������[ReadonlyStatic]����ʾ���ɶ�����ֵ��
���ھ�̬������ֻ��û�в����ľ�̬�������Դ���[VoidStaticMethod]����ʱ�����ڼ�������ͨ����ť������ʽ���á�
</br></br>