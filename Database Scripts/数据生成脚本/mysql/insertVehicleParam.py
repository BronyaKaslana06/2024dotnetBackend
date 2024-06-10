import pymysql
from pymysql.constants import CLIENT
import os

def read_image(file_path):
    """读取图片文件并返回二进制数据"""
    with open(file_path, 'rb') as file:
        return file.read()

# 连接到数据库
connection = pymysql.connect(host='110.40.172.207',
                             user='dotnet',
                             password='X6ajhshHW45ijjfF',
                             db='dotnet',
                             client_flag=CLIENT.MULTI_STATEMENTS,
                             cursorclass=pymysql.cursors.DictCursor)
try:
    with connection.cursor() as cursor:
        # 创建表
        # cursor.execute("""
        #     CREATE TABLE IF NOT EXISTS `VEHICLE_PARAM` (
        #         `VEHICLE_MODEL` VARCHAR(10),
        #         `ModelName` VARCHAR(255),
        #         `TRANSMISSION` VARCHAR(255),
        #         `SERVICE_TERM` DATETIME,
        #         `MANUFACTURER` VARCHAR(255),
        #         `MAX_SPEED` INT,
        #         `SNIP` BLOB
        #     );
        # """)
        # 图片文件路径
        images_path = '.\\carPic'
        images = ['ec6-desktop.png', 'es7-desktop.png', 'et5-desktop.png', 'et7-desktop.png',
                  'vehicle-slider-ec7-desktop.jpg', 'vehicle-slider-es6-desktop.png',
                  'vehicle-slider-es8-prime-desktop.jpg', 'vehicle-slider-et5-touring-desktop.png']
        
        # 插入数据
        for index, image_name in enumerate(images, start=1):
            file_path = os.path.join(images_path, image_name)
            binary_data = read_image(file_path)
            cursor.execute("""
                INSERT INTO `VEHICLE_PARAM` (
                    `VEHICLE_MODEL`, `ModelName`, `TRANSMISSION`, `SERVICE_TERM`, `MANUFACTURER`, `MAX_SPEED`, `SINP`
                ) VALUES (%s, %s, %s, %s, %s, %s, %s)
            """, (
                str(index), f'蔚来{image_name[:-10]}', '感应异步电机', '2020-01-01 00:00:00',
                '蔚来电驱动系统制造基地（南京）', 350, binary_data
            ))
        # 提交事务
        connection.commit()
        print('插入成功')
except Exception as e:
    print(f"发生错误: {e}")
    connection.rollback()
finally:
    connection.close()

