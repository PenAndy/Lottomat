using System;
using Lottomat.Application.Entity.SystemManage;
using Lottomat.Application.IService.SystemManage;
using Lottomat.Data.Repository;
using Lottomat.Util.WebControl;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Lottomat.Application.Code;
using Lottomat.Application.Entity.LotteryNumberManage;
using Lottomat.Util.Extension;
using Newtonsoft.Json.Linq;

namespace Lottomat.Application.Service.SystemManage
{
    /// <summary>
    /// �� �� 1.0
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2017-12-06 13:49
    /// �� ������������
    /// </summary>
    public class BaseFriendLinksService : RepositoryFactory<BaseFriendLinksEntity>, IBaseFriendLinksService
    {
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<BaseFriendLinksEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<BaseFriendLinksEntity>();
            JObject queryParam = queryJson.ToJObject();
            if (queryParam != null)
            {
                if (!queryParam["Name"].IsEmpty())
                {
                    string Name = queryParam["Name"].ToStringEx();
                    expression = expression.And(t => t.Name.Equals(Name));
                }
                if (!queryParam["Type"].IsEmpty())
                {
                    string Type = queryParam["Type"].ToStringEx();
                    expression = expression.And(t => t.Type.Equals(Type));
                }
            }

            return this.BaseRepository().FindList(expression, pagination);
        }
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<BaseFriendLinksEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="condition">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<BaseFriendLinksEntity> GetList(Expression<Func<BaseFriendLinksEntity, bool>> condition)
        {
            return this.BaseRepository().FindList(condition);
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public BaseFriendLinksEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region �ύ����
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValue">����</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, BaseFriendLinksEntity entity,string code)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                if (code == "1")
                {
                    entity.IsEnable = true;
                }
                else if (code == "0")
                {
                    entity.IsEnable = false;
                }

                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                entity.Create();
                this.BaseRepository().Insert(entity);
            }
        }
        #endregion
    }
}
