using System;
using System.Collections.Generic;
using System.Linq;
//����ʱ�䣺 2013-9-26
//�����ˣ� л��
//������ ʵ�幫������
namespace Mem.Core
{
    /// <summary>
    /// ʵ���������
    /// </summary>
    public class BaseEntity<T>
    {
        /// <summary>
        /// ����ʵ���ʶ�� �����еı�ı�ʾ������Id������Mem.Domain ���̳������崴������
        /// </summary>
        public long Id { get; set; }

    }
}
