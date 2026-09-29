.class public interface abstract Lcom/zopim/android/sdk/data/DataSource;
.super Ljava/lang/Object;


# virtual methods
.method public abstract addAccountObserver(Ljava/util/Observer;)V
.end method

.method public abstract addAgentsObserver(Ljava/util/Observer;)V
.end method

.method public abstract addChatLogObserver(Ljava/util/Observer;)V
.end method

.method public abstract addConnectionObserver(Ljava/util/Observer;)V
.end method

.method public abstract addDepartmentsObserver(Ljava/util/Observer;)V
.end method

.method public abstract addFormsObserver(Ljava/util/Observer;)V
.end method

.method public abstract addProfileObserver(Ljava/util/Observer;)V
.end method

.method public abstract clear()V
.end method

.method public abstract deleteAccountObserver(Ljava/util/Observer;)V
.end method

.method public abstract deleteAgentsObserver(Ljava/util/Observer;)V
.end method

.method public abstract deleteChatLogObserver(Ljava/util/Observer;)V
.end method

.method public abstract deleteConnectionObserver(Ljava/util/Observer;)V
.end method

.method public abstract deleteDepartmentsObserver(Ljava/util/Observer;)V
.end method

.method public abstract deleteFormsObserver(Ljava/util/Observer;)V
.end method

.method public abstract deleteObservers()V
.end method

.method public abstract deleteProfileObserver(Ljava/util/Observer;)V
.end method

.method public abstract getAccount()Lcom/zopim/android/sdk/model/Account;
.end method

.method public abstract getAgents()Ljava/util/LinkedHashMap;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/LinkedHashMap<",
            "Ljava/lang/String;",
            "Lcom/zopim/android/sdk/model/Agent;",
            ">;"
        }
    .end annotation
.end method

.method public abstract getChatLog()Ljava/util/LinkedHashMap;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/LinkedHashMap<",
            "Ljava/lang/String;",
            "Lcom/zopim/android/sdk/model/ChatLog;",
            ">;"
        }
    .end annotation
.end method

.method public abstract getConnection()Lcom/zopim/android/sdk/model/Connection;
.end method

.method public abstract getDepartments()Ljava/util/Map;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Lcom/zopim/android/sdk/model/Department;",
            ">;"
        }
    .end annotation
.end method

.method public abstract getForms()Lcom/zopim/android/sdk/model/Forms;
.end method

.method public abstract getProfile()Lcom/zopim/android/sdk/model/Profile;
.end method
