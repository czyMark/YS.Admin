
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.Fonts;

namespace YS.Admin.Util
{
    public class CaptchaHelper2
    {
        #region 得到验证码
        /// <summary>
        /// Tuple第一个值是表达式，第二个值是表达式结果
        /// </summary>
        /// <returns></returns>
        public static Tuple<string, int> GetCaptchaCode()
        {
            int value = 0;
            char[] operators = { '+', '-', '*' };
            string randomCode = string.Empty;
            Random random = new Random();

            int first = random.Next() % 10;
            int second = random.Next() % 10;
            char operatorChar = operators[random.Next(0, operators.Length)];
            switch (operatorChar)
            {
                case '+': value = first + second; break;
                case '-':
                    // 第1个数要大于第二个数
                    if (first < second)
                    {
                        int temp = first;
                        first = second;
                        second = temp;
                    }
                    value = first - second;
                    break;
                case '*': value = first * second; break;
            }

            char code = (char)('0' + (char)first);
            randomCode += code;
            randomCode += operatorChar;
            code = (char)('0' + (char)second);
            randomCode += code;
            randomCode += "=?";
            return new Tuple<string, int>(randomCode, value);
        }
        #endregion
        public static byte[] CreateCaptchaImage(string randomCode)
        {
            const int randAngle = 45; // 随机转动角度
            int mapwidth = randomCode.Length * 16;
            int mapheight = 28;

            // 创建图片背景
            using (Image<Rgba32> image = new Image<Rgba32>(mapwidth, mapheight))
            {
                // 填充背景
                image.Mutate(ctx => ctx.Fill(Color.White));

                Random random = new Random();
                Color[] colors = { Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black };

                // 定义字体集合
                //string[] fonts = { "宋体" };
                //FontCollection collection = new FontCollection();
                //foreach (var fontPath in fonts)
                //{
                //    collection.Add(fontPath);
                //}

                // 定义字体样式
                //FontFamily fontFamily = collection.Add("宋体");// collection.Find("Arial");
                //Font font = fontFamily.CreateFont(14, FontStyle.Bold);
                Font font = SystemFonts.CreateFont("Arial", 15, FontStyle.Bold);
               
                // 绘制验证码字符
                PointF center = new PointF(mapwidth / 2, mapheight / 2-5);
                char[] chars = randomCode.ToCharArray();

                for (int i = 0; i < chars.Length; i++)
                {
                    float angle = random.Next(-randAngle, randAngle); // 转动的度数
                    string character = chars[i].ToString();

                    // 计算每个字符的位置
                    PointF location = new PointF(i * 16 + 5, mapheight / 2 - 5);

                    // 设置绘图样式
                    DrawingOptions options = new DrawingOptions
                    {
                        GraphicsOptions = new GraphicsOptions
                        {
                            Antialias = true,
                            BlendPercentage = 1
                        }
                    };

                    // 绘制字符
                    image.Mutate(ctx => ctx
                        //.Rotate(angle) // 旋转字符
                        .DrawText(options, character, font, colors[i % colors.Length], location));
                        //.Rotate(-angle)); // 旋转回去
                }

                // 保存图片到内存流
                using (MemoryStream ms = new MemoryStream())
                {
                    image.Save(ms, new GifEncoder());
                    return ms.ToArray();
                }

            }
        }
    }
}
