using System;
using System.Collections.Generic;
using System.Text;

namespace TeamCores.Common.Utilities
{
    sealed class IDCreater
    {
        /**
         本程序可以生成64位全局唯一ID，适合在分布式服务器集群中产生唯一ID
       　特点比微软自带的GUID要节省一倍的空间，即只需要64位的int即可，因此基本上可以抛弃微软的那个GUID了
         支持每秒生成65536个,在此条件基础上这辈子都不会产生相同的ID

         由 时间戳+服务器号+平台号+本地递增序号 组成

         时间戳 32bit
         服务分区号 8bit
         服务器编号 8bit
         递增序号 16bit 

            支持大区256（0-255）个，每个大区下支持256（0-255）个小区，每个小区里支持每秒生成65536个唯一ID
       */
        private static ulong mark_timestamp = 0xffffffff00000000;/*时间戳掩码*/
        private static ulong mark_district = 0x00000000ff000000;/*服务器分区掩码*/
        private static ulong mark_servers = 0x0000000000ff0000;/*服务器编号掩码*/
        private static ulong mark_base = 0x000000000000ffff;/*本地ID编号掩码*/
        private static uint baseId = 0;/*本地ID*/

        /// <summary>
        /// 生成整个世界里全局唯一的ID ,服务器号:district（0-254），平台号:platform（0-254） [如果只有一个平台，则此平台号可以是服务器集群里的服务器类型编号]
        /// </summary>
        /// <param name="platform"></param>
        /// <param name="district"></param>
        /// <returns></returns>
        public static ulong NewId(uint platform, uint district)
        {
            ulong timeStamp = (ulong)TimeGen();
            ulong newId = ((timeStamp << 32) & mark_timestamp) | ((district << 24) & mark_district) | ((platform << 16) & mark_servers) | (baseId & mark_base);
            baseId++;
            return newId;
        }

        private static readonly DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// //获取自古以来的时间戳
        /// </summary>
        /// <returns></returns>
        private static long TimeGen()
        {
            return (long)(DateTime.UtcNow - Jan1st1970).TotalSeconds;
        }

        /// <summary>
        /// 根据ID 逆向获取服务器分区和服务器ID信息
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="type"></param>
        /// <param name="id"></param>
        public static void GetServer(ulong guid, out uint type, out uint id)
        {
            type = (uint)((guid & mark_district) >> 24);
            id = (uint)((guid & mark_servers) >> 16);
        }

    }
}
