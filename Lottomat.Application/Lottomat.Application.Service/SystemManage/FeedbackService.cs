using Lottomat.Application.Entity.SystemManage;
using Lottomat.Application.IService.SystemManage;
using Lottomat.Data.Repository;
using Lottomat.Util.WebControl;
using System.Collections.Generic;
using System.Linq;
using Lottomat.Application.Code;
using Lottomat.Application.Entity.LotteryNumberManage;
using Lottomat.Util.Extension;
using Lottomat.Utils.Date;
using Newtonsoft.Json.Linq;

namespace Lottomat.Application.Service.SystemManage
{
    /// <summary>
    /// �� �� 1.0
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2017-12-18 10:55
    /// �� �����������
    /// </summary>
    public class FeedbackService : RepositoryFactory<FeedbackEntity>, IFeedbackService
    {
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<FeedbackEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<FeedbackEntity>();
            JObject queryParam = queryJson.ToJObject();
            if (queryParam != null)
            {
                if (!queryParam["NickName"].IsEmpty())
                {
                    string NickName = queryParam["NickName"].ToString();
                    expression = expression.And(t => t.NickName.Contains(NickName));
                }
                if (!queryParam["Contact"].IsEmpty())
                {
                    string Contact = queryParam["Contact"].ToString();
                    expression = expression.And(t => t.Contact.Contains(Contact));
                }
            }

            return this.BaseRepository().FindList(expression, pagination);
        }
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<FeedbackEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public FeedbackEntity GetEntity(string keyValue)
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
        public int SaveForm(string keyValue, FeedbackEntity entity, string which)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                if (which == "1")
                {
                    entity.IsReply = true;
                    entity.ReplyTime = DateTimeHelper.Now;
                    entity.ReplyUserName = entity.ReplyUserName == ""
                        ? OperatorProvider.Provider.Current().UserName
                        : entity.ReplyUserName;
                }
                entity.Modify(keyValue);
                return this.BaseRepository().Update(entity);
            }
            else
            {
                entity.Create();
                return this.BaseRepository().Insert(entity);
            }
        }
        #endregion
    }
}
