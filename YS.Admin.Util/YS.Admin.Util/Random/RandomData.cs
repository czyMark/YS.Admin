using System;
using System.Collections.Generic;

namespace YS.Admin.Util
{

    public class RandomData
    {
        /// <summary>
        /// 随机数
        /// </summary>

        private static RandomData instance;
        public static RandomData Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RandomData();
                }
                return instance;
            }
        }



        /// <summary>
        /// 世界货币单位
        /// </summary>
        public Dictionary<string, string> MoneyUnit =new Dictionary<string, string>();


        /// <summary>
        /// 轮船交通工具
        /// </summary>
        public string[] Ships;
        /// <summary>
        /// 飞行交通工具
        /// </summary>
        public string[] AirPlanes;
        /// <summary>
        /// 陆地交通工具
        /// </summary>
        public string[] Transportations;


        /// <summary>
        /// 公司涉及的单据
        /// </summary>
        public string[] Receipts;

        /// <summary>
        /// 公司各个部门名称
        /// </summary>
        public string[] DepertmentNames;

        /// <summary>
        /// 百家姓中的单姓
        /// </summary>
        public string[] SingleNames;

        /// <summary>
        /// 百家姓中的复姓
        /// </summary>
        public string[] DoubleNames;

        /// <summary>
        /// 百家姓中的所有姓氏
        /// </summary>
        public string[] AllNames;

        /// <summary>
        /// 常见名
        /// </summary>
        public string[] Names;

        /// <summary>
        /// 电信手机号开头
        /// </summary>
        public string[] CTC;


        /// <summary>
        /// 联通手机号开头
        /// </summary>
        public string[] CUCC;


        /// <summary>
        /// 移动手机号开头
        /// </summary>
        public string[] CMCC;


        /// <summary>
        /// 所有地区
        /// </summary>
        public string[] Region;
        /// <summary>
        /// 省
        /// </summary>
        public Dictionary<string, string> Province = new Dictionary<string, string>();
        /// <summary>
        /// 省对应的简称
        /// </summary>
        public Dictionary<string, string> ProvinceNative = new Dictionary<string, string>();
        /// <summary>
        /// 城市
        /// </summary>
        public Dictionary<string, string> City = new Dictionary<string, string>();

        /// <summary>
        /// 邮箱
        /// </summary>
        public string[] EMail;


        /// <summary>
        /// 行政编号
        /// </summary>
        public string[] RegionCode;
        /// <summary>
        /// 职位
        /// </summary>
        public string[] ITPosition;

        /// <summary>
        /// 常见成语
        /// </summary>
        public string[] Idioms;


        /// <summary>
        /// 城市机构
        /// </summary>
        public string[] CityInstitution;

        /// <summary>
        /// 中央部门机构
        /// </summary>
        public string[] CentralGovInstitution;

        private RandomData()
        {
            #region 姓名

            string temp = "赵,钱,孙,李,周,吴,郑,王,冯,陈,褚,卫,蒋,沈,韩,杨,朱,秦,尤,许,何,吕,施,张,孔,曹,严,华,金,魏,陶,姜,戚,谢,邹,喻,柏,水,窦,章,云,苏,潘,葛,奚,范,彭,郎,鲁,韦,昌,马,苗,凤,花,方,俞,任,袁,柳,酆,鲍,史,唐,费,廉,岑,薛,雷,贺,倪,汤,滕,殷,罗,毕,郝,邬,安,常,乐,于,时,傅,皮,卞,齐,康,伍,余,元,卜,顾,孟,平,黄,和,穆,萧,尹,姚,邵,湛,汪,祁,毛,禹,狄,米,贝,明,臧,计,伏,成,戴,谈,宋,茅,庞,熊,纪,舒,屈,项,祝,董,梁,杜,阮,蓝,闵,席,季,麻,强,贾,路,娄,危,江,童,颜,郭,梅,盛,林,刁,钟,徐,邱,骆,高,夏,蔡,田,樊,胡,凌,霍,虞,万,支,柯,昝,管,卢,莫,经,房,裘,缪,干,解,应,宗,丁,宣,贲,邓,郁,单,杭,洪,包,诸,左,石,崔,吉,钮,龚,程,嵇,邢,滑,裴,陆,荣,翁,荀,羊,於,惠,甄,曲,家,封,芮,羿,储,靳,汲,邴,糜,松,井,段,富,巫,乌,焦,巴,弓,牧,隗,山,谷,车,侯,宓,蓬,全,郗,班,仰,秋,仲,伊,宫,宁,仇,栾,暴,甘,钭,历,戎,祖,武,符,刘,景,詹,束,龙,叶,幸,司,韶,郜,黎,蓟,溥,印,宿,白,怀,蒲,邰,从,鄂,索,咸,籍,赖,卓,蔺,屠,蒙,池,乔,阳,郁,胥,能,苍,双,闻,莘,党,翟,谭,贡,劳,逄,姬,申,扶,堵,冉,宰,郦,雍,卻,璩,桑,桂,濮,牛,寿,通,边,扈,燕,冀,姓,浦,尚,农,温,别,庄,晏,柴,瞿,阎,充,慕,连,茹,习,宦,艾,鱼,容,向,古,易,慎,戈,廖,庾,终,暨,居,衡,步,都,耿,满,弘,匡,国,文,寇,广,禄,阙,东,欧,殳,沃,利,蔚,越,夔,隆,师,巩,厍,聂,晁,勾,敖,融,冷,訾,辛,阚,那,简,饶,空,曾,毋,沙,乜,养,鞠,须,丰,巢,关,蒯,相,查,后,荆,红,游,竺,权,逮,盍,益,桓,公";
            SingleNames = temp.Split(',');
            temp = "万俟,司马,上官,欧阳,夏侯,诸葛,闻人,东方,赫连,皇甫,尉迟,公羊,澹台,公冶,宗政,濮阳,淳于,单于,太叔,申屠,公孙,仲孙,轩辕,令狐,徐离,宇文,长孙,慕容,司徒,司空";

            DoubleNames = temp.Split(',');
            AllNames = new string[SingleNames.Length + DoubleNames.Length];
            for (int i = 0; i < SingleNames.Length; i++)
            {
                AllNames[i] = SingleNames[i];
            }
            for (int i = 0; i < DoubleNames.Length; i++)
            {
                AllNames[SingleNames.Length + i] = DoubleNames[i];
            }
            temp = "庆雪,启迪,心然,雨霏,涵诺,飞凡,梓炜,惠芬,晓东,晨婷,荺汀,跳跳,仕龙,煥义,裕连,秋明,嵩杰,睿轩,轶群,歆捷,柏晗,楚洋,赫男,名宴,骄阳,连付,芯瑜,楚升,煜菲,瑞驰,傲熙,佳豪,裕峥,飞飞,颡兴,淑轩,文恒,欧楠,羽薇,龙辰,纪言,书德,柳诺,明宏,一轩,润生,旭明,圣楠,博赡,良程,昕晴,楷硕,辰龙,圳矜,冰鑫,清发,延夏,欣汝,振宇,若欣,昌聪,金君,研霓,赞浚,文彦,一轩,金颖,启芮,思彤,洛唏,锦涛,哲極,北峰,朋蕾,馨茹,子雨,征鲤,子彦,晨旭,苈殿,添睿,梓强,晨阳,阮变,春红,谦桐,一帅,嘉畅,宇萱,圣盛,冰瑜,杳篱,毅騉,舣显,昊苍,雅倩,毕友,豪锋,卫锋,春蕾,泓乐,湘渝,紫嫣,浩博,曜儿,一新,嘉明,百漩,碧婷,企志,艺馨,溪桐,力横,鑫源,倩雪,黎钦,益智,静轩,粟琳,子健,觉豪,金娜,先忍,传鸿,鹏涛,雨婷,玉洁,宏博,悠悠,晓萍,幕晞,鸿哲,玲睿,汉宸,铱卉,成诚,雅鑫,智芳,睿林,劳逸,朔骧,鑫研,昕彤,若晗,克军,静文,常路,浩蕊,志朗,至德,倍汶,康年,卜晨,昕梦,新畅,雅彤,炎劬,红叶,武涛,土纠,沛雯,姿岚,斌翼,佩雅,一如,冲牧,子皓,雅俐,工煜,礼鸿,敖恒,希晟,廷昊,绍芹,朋淏,丽晓,响纸,芷婧,红新,到小,谙易,雨笙,言柔,辞舒,嘉璋,丹华,矗朋,利财,雨萌,丽慧,宸泽,瑞杰,传幸,靖琪,跃明,字莟,艺淏,子成,圣尧,明桦,湛淼,浛琳,宸瑞,誉官,泽民,庆南,雅熙,希焱,巾瑞,大芹,妍丁,沁池,奕博,朝仁,韦烜,高昂,振南,佳泽,岩泓,裕喆,俊基,赛赛,嘉旭,硕桢,博涵,郁秦,安弘,启正,佳悦,槐英,裕亮,铭阳,连山,舒雨,仪涵,浩营,佑明,紫仪,弘毅,正康,金凤,联鸿,小粟,宽钊,腾泽,哲侑,嘉濠,金河,瑶瑶,銮天,水淼,晟熙,博翰,景龙,傲君,小迪,晨瑜,年烨,昇睿,霈珺,潇泽,涵羲,燕兰,抒辰,佳谦,仁远,新玲,龙泽,泽晞,艺舫,阳怡,家榕,昞吉,维哲,晟霖,神州,昶蛑,佩华,向东,梓莘,佳雨,睿滔,超凡,恒贵,忻楠,辰海,如源,竣玮,宝艺,炜行,炳丁,河沐,正坤,崞瑙,敖征,清睿,英超,先书,晨峰,子权,曦熙,弘图,希霆,晨訏,锡海,瑞溯,苏涵,朋德,欣瑜,柳宁,艺婷,秋芳,孩伺,铃绳,帏遵,清澈,丽娜,子键,宝萱,和蔼,祥溢,昊洲,启恒,海华,浩熠,晓琳,婉轩,艺涵,继轩,镇海,瀚宇,峻楠,妍艳,睿鑫,鸿煊,润皑,梓炜,惠芬,晓东,晨婷,荺汀,跳跳,雅林,裕涵,伟诚,明轩,毅杨,佟裴,瑾轩,翰一,玥彤,朝华,承眧,梓澧,梓灿,岚玺,博宇,少婷,玥晨,文昭,佳旭,雪晴,兴棒,梓滢,铴均,海超,传钧,涵容,腾腾,鸿远,会茹,睿博,昊雨,金溪,惋钥,澍域,天惠,子耀,远海,缤翌,粟燊,颖潼,屹俊,卓璇,昊旭,贺琳,傲珺,祖瀚,艺华,恩旭,世恒,基钰,又又,益轩,宇昊,梓悦,蔡齐,涵仪,宇昂,泽瑄,梅彤,力蔺,智恒,静轩,雅文,益华,峻贤,志姚,翼程,龙归,一袁,伟德,文千,觉农,春豪,火南,博允,涵悦,晶婧,梓轩,锣使,琪博,烁鸿,圣泽,玺豪,瑞澜,才艺,庆宝,宇卓,成桥,启源,锦绣,靖茹,钰喆,炜茵,思圣,露颖,东想,婵英,宙抑,思颖,鎔洲,远亮,佳彤,子探,佳皓,拥天,子羽,广雷,睿昶,钊锐,皓博,谚熙,佐豪,思淇,博林,秋茵,兆江,腾月,箐苗,雨莺,焕发,卜晨,佳琳,义泽,奇俊,铭瑞,瑞洋,祥博,红梅,冰兰,豪轩,建业,地财,翊桦,冠阅,语瞳,荣昙,轾恒,佳琦,沄欣,朋蔚,晓娟,澄澄,洪九,海露,栢玮,涵育,邦浩,信俞,宗鹏,智勋,文硕,齐佐,依凡,香凝,思远,正帆,祺媛,熔雯,亦煊,晨菥,子缮,延责,传豪,佳芸,依伊,晨诺,韬译,诗轩,娟欣,婷可,芬依,留瑛,培娟,虞婉,童玲,矩文,悦中,濮文,善美,晖娅,莹香,哓婧,梅霎,当玲,叶瑛,三玉,婷茵,燕斜,豫瑞,埠洁,沛源,二萍,彩媛,燮莹,典萍,梓婷,绳琳,璐云,球芳,莹麟,奥芬,彭美,洛伊,宫文,苗莹,郭梅,婷蛎,娟娟,育莹,卉妹,军娟,桐洁,琴陈,莹秀,昱媛,怡兰,尚瑶,玲借,些忆,慧欢,沫婷,爽娜,燕亭,召花,倩夷,陶怡,俏雪,都琴,萤文,保秀,珂莹,憧琳,续裴,序霞,腮蓉,城芳,娟嘉,怡蓝,厉娟,贞瑶,映萱,席萍,秀隰,路花,文婷,艿艳,渌芳,悦合,芬露,晴岚,秀忻,梓瑶,亿婧,蓉端,梦妙,钏尼,箢文,沭蓉,务英,紫若,渊文,柬玲,绘芬,烽丽,婕婵,洁佳,妍蒂,迁文,贯洁,海熙,亚锦,岚洁,薪茹,雪熙,莉溶,根妍,沈怡,德娜,涞莉,曼烟,桀文,媛宜,裔娟,沅娟,燕钢,蓉钻,珞莹,沁雨,臣文,欣芙,桂依,明丽,雯鸣,至怡,瑛裕,汶瑶,旨悦,桠蓉,娟茹,良美,艺茹,蕈辂,伊倩,吟梅,彤秀,沂芬,泓琴,娜轩,淑榆,炳娥,西蓉,余丽,舜瑶,璋妍,荣英,魅倩,婧霞,妮莲,由英,珏悦,佩明,琳倍,玉妹,球英,丽彤,平妹,娟阁,婧攸,苣文,荷怡,栋倩,晓怡,让悦,竹芳,倚岚,甜洁,莹伟,曦颖,銮菀,享英,蓉蔓,粟霞,义莉,朵拼,霍文,文琼,娅沁,写文,钧玉,茂瑛,铭琼,曦丽,嚣妍,非妍,寒怡,倩珑,莉扬,素佳,刘瑛,渝轩,牡芬,冰怡,苓蓓,莉萨,丽琼,静蓉,外芳,昕芬,殷秀,蕊英,秀染,松妍,二媛,憎怡,坷艳,溯娟,昕梦,凝阳,潍婧,燕昭,雪琦,怀霞,珠听,倩曼,腮蓉,城芳,娟嘉,怡蓝,厉娟,贞瑶,滢文,平丽,岫婷,茕文,愉琴,皙萍,媛迤,申妞,邺琳,琳琦,熙雯,沐妮,捷莹,凰玉,萱琳,鑫嫣,秀熹,芳宛,格颖,灏玉,雍丽,其妹,蝉莹,碧霓,厚美,瑞贞,旦怡,潞梅,梦娅,及玉,希瑶,馗文,宣英,琳馥,静美,咚丽,菱荔,暄茹,兆丽,犹美,采丽,柔雪,仨艳,雍霞,倩芝,鹰倩,艳玲,博娅,秋嘉,秀霆,蜻芳,寅艳,琴妙,添娥,琳羽,玢琳,云萍,若禧,妍宏,祺婷,愈红,裔莹,茹加,炫娜,熔倩,金凤,芳榆,鸾玉,宇红,上美,腊媛,婧揉,瑶彡,素钰,宫瑛,悔霞,馨芳,蒙燕,利莉,婷毓,莹郦,泾瑛,芹雪,化娥,秋花,淞动,莹四,稚娅,穗雪,映娟,生雪,里英,娅儇,岈娟,霞冰,长颖,陈娜,莘琴,孜梅,茹纬,钱媛,珠荣,郁娟,妍桦,缘桑,娅忻,婕丽,洋丽,聍文,娟镅,琳特,赴霞,任千,桓琼,柿红,明佳,雯嘉,赛瑛,璐芝,中郡,念桃,钰佳,欣昕,菏娟,竣倩,闰梅,诩艳,舂婧,若婷,昱艳,裕琳,俞君,昱茹,镱颖,晶馨,晋颖,观玉,花萍,茹镱,琳婷,诗楠,仔婧,瑛立,霞俊,旖舷,郜妍,召芬,觯颖,笙悦,娅叙,丛芸,琳皖,芬远,单怡,捷萍,萁悦,悦枚,玲峥,温媛,谦蓉,琳娉,博琴,媛袁,见梅,珺云,彗芳,尹茹,菱悦,辉玉,荔琴,远琴,秀圆,祉妍,茹敬,颀倩,侠蓉,妍忠,飞婵,同玉,漫怡,沃蓉,绘妍,珊倩,婧影,曼钰,敏琳,广冉,冰玥,品梅,芷嫣,弼莹,炀洁,百妍,纱莉,玲彦,琳凇,倩纭,彤少,励花,瑶藜,蔷琳,瑰花,韵艳,修花,琳鳞,圆洁,晓语,娅惟,夜芬,悦沐,锦涵,霞颖,枫洁,荷悦,蓣婷,怡人,诚倩,玲玲,润玉,汀婵,锵萍,宵莹,怡佾,蕊悦,楼瑛,旦萍,麟娥,一妍,钦蓉,芳冰,伽美,私蓉,瑛帅,关艳,波蓉,忆梅,祯妍,萍华,润幽,雩瑶,诩艳,舂婧,若婷,昱艳,裕琳,俞君,音美,妍焰,煦婵,显燕,议茹,闻琳,味芳,悦萱,卉燕,诗雪,琳牧,莉媚,育梅,婧宜,骏文,映莉,慈梅,芳瑾,昌文,琳诗,弋玉,镁莉,漫瑶,慈颖,馥霞,妮黛,几莉,潍美,诘冉,玲淋,雨妙,媛如,妍嬉,莹飞,倩姣,芷巧,北燕,琳名,琳玟,梦璐,以菱,炀丽,伦玉,钰雅,欣瑶,坤文,欲蓉,朵雯,高梅,浠颖,寒瑜,媛郴,熙莹,莹锌,岘婷,欢霜,梨莉,沛妤,砚琼,悦祖,琳硕,治玉,嗳琴,芬清,铭英,阮婷,曦悦,悦枷,婵葶,小梅,幸艳,刚琴,逸娟,用玲,泯燕,麒琳,婧祺,俸颖,雪玲,长雪,振玉,璀瑶,抒美,莉湄,祥琳,釜茹,潆妍,缌瑶,艳莉,弘玲,嫣阳,欣青,云嫦,嫣阑,恺婷,雯菲,英诗,婷硕,书琼,璐莹,桦媛,协娥,岂文,怡娟,尤琼,倚芬,琦淑,卓婷,亭蓉,夷琼,寄瑶,睦萍,艳嫣,悦仟,小红,凯妞,伦颖,琳钥,帛文,璐蓉,秀玑,原莉,莫琳,仪莹,冉娜,乔英,甜绿,泓莹,倩同,陟文,氏艳,佳恬,义机,妁悦,茵曦,研红,绎洁,布梅,紫娇,欣润,琳彤,习霞,伊琴,营雪,森,泰,信,冉,宏,弛,婷,恺,中,杉,洋,敏,治,桃,北,娟,勋,润,壹,山,宓,余,暄,欢,曦,新,初,凤,弘,月,沁,承,亮,微,尘,含,波,樱,哲,槿,媛,启,湙,伟,丞,扬,淇,岑,彬,丽,江,晴,七,岚,昊,渤,希,宥,正,庚,泉,喆,永,影,尧,搏,本,亭,善,德,云,昆,和,嫒,文,心,杰,强,昭,楸,曈,志,晨,书,宇,橙,坤,松,懿,乐,夏,子,娴,争,俊,枫,墉,天,妙,彭,妍,晓,悦,晶,可,榕,淼,慈,亿,妤,沐,园,宸,嫣,忆,泽,一,傲,棋,杭,沂,圆,柔,权,伶,名,义,川,伦,彰,岛,昂,桐,彦,嫚,朴,婴,娅,兮,汀,墨,婧,易,沫,浩,帆,柏,佑,君,映,容,城,意,偲,刚,晗,愉,望,丹,楷,宵,才,东,清,斯,梦,星,湖,昕,斌,晔,恩,允,勇,武,棣,坷,成,儒,予,恒,歌,桦,倩,林,婉,卿,栩,民,汐,征,汜,楠,梓,国,姝,廷,栋,冰,严,洁,泓,任,佳,宜,兴,彤,多,彧,少,宁,振,旗,依,于,晋,桁,华,嵩,晟,明,奕,杨,朵,景,毅,元,惠,旭,春,仪,淳,宝,夕,槊,旺,商,歆,梵,果,仁,沣,旻,浠,洛,涵,峰,兰,安,培,格,乾,伊,康,怡,延,卓,妮,政,涛,凯,屹,呈,富,二,如,博,慧,佛,同,儿,匀,奇,斐,之,亚,昱,婕,乔,平,垚,思,欣,凡,渊,庭,娜,洪,嘉,智,怀";
            Names = temp.Split(',');
            #endregion
             

        }

        #region 统计局指标项

        /// <summary>
        /// 统计局指标项
        /// </summary>
        public string[] TotalIndexs()
        {
            //读取本地的json获取统计局指标并返回
            return new string[1];
        }
        /// <summary>
        /// 统计局细项指标
        /// </summary>
        public string[] TotalSubIndexs()
        {
            //读取本地的json获取统计局指标并返回
            return new string[1];
        }
        /// <summary>
        /// 统计局细项指标
        /// </summary>
        /// <param name="indexName">统计局指标项名称，用于匹配指标项下的所有指标</param>
        /// <returns></returns>
        public string[] TotalSubIndexs(string indexName)
        {
            //读取本地的json获取统计局指标并返回
            return new string[1];
        }
        #endregion



        public string ChineseName()
        {
            return Tool.GetVcodeNum(1, AllNames) + Tool.GetVcodeNum(1, Names);
        }

        public string CTCNumber()
        {
            return Tool.GetVcodeNum(1, CTC) + Tool.GetNum(9);
        }

        public string CUCCNumber()
        {
            return Tool.GetVcodeNum(1, CUCC) + Tool.GetNum(9);
        }

        public string CMCCNumber()
        {
            return Tool.GetVcodeNum(1, CMCC) + Tool.GetNum(9);
        }


        public string PhoneEmailNumber()
        {
            return CMCCNumber() + Tool.GetVcodeNum(1, EMail);
        }

        public string CharEmailNumber()
        {
            return Tool.GetVcodeNum(8) + Tool.GetVcodeNum(1, EMail);
        }

        public string IDCodeNumber()
        {
            return Tool.GetVcodeNum(1, RegionCode) + YearNumber() + MonthNumber() + Tool.GetNum(4);
        }

        public string YearNumber()
        {
            return Tool.GetNum(DateTime.MinValue.Year, DateTime.Now.AddYears(-18).Year);
        }

        public string MonthNumber()
        {
            return Tool.GetNum(1, 12).PadLeft(2, '0');
        }

        public string DayNumber()
        {
            return Tool.GetNum(1, 31).PadLeft(2, '0');
        }

        /// <summary>
        /// 随机True,False
        /// </summary> 
        /// <returns></returns>
        public bool StateData()
        {
            int t = Tool.GetSimpNum(1, 9999);
            return t % 2 == 0;
        }
        public DateTime DataNumber(int day)
        {
            return DateTime.Now.AddDays(Tool.GetSimpNum(1, day) * -1);
        }

        /// <summary>
        /// 矩形内随机一个点
        /// </summary>
        /// <param name="x1">X坐标最小</param>
        /// <param name="x2">X坐标最大</param>
        /// <param name="y1">Y坐标最小</param>
        /// <param name="y2">Y坐标最大</param>
        /// <returns></returns>
        public int[] RectanglePoint(int x1, int x2, int y1, int y2)
        {
            return new int[] { Tool.GetSimpNum(x1, x2), Tool.GetSimpNum(y1, y2) };
        }
        /// <summary>
        /// 获取护照号
        /// 公务护照号码的格式为：SE+7 位数字编码（以S开头的护照是代表公务护照）。
        /// 外交护照号码的格式为：DE+7 位数字编码（以D开头的护照是代表外交护照）。
        /// 公务普通护照号码的格式为：PE+7 位数字编码（以P开头的护照是代表公务普通护照）。
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public string IDPostCodeNumber(string format = "PE")
        {
            return format + Tool.GetNum(7);
        }
    }
}
