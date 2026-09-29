.class Lcom/helpshift/JavaCore$1;
.super Lcom/helpshift/common/domain/F;
.source "JavaCore.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/JavaCore;->fetchServerConfig()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/JavaCore;


# direct methods
.method constructor <init>(Lcom/helpshift/JavaCore;)V
    .locals 0

    .line 196
    iput-object p1, p0, Lcom/helpshift/JavaCore$1;->this$0:Lcom/helpshift/JavaCore;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 5

    .line 199
    iget-object v0, p0, Lcom/helpshift/JavaCore$1;->this$0:Lcom/helpshift/JavaCore;

    invoke-static {v0}, Lcom/helpshift/JavaCore;->access$000(Lcom/helpshift/JavaCore;)Lcom/helpshift/account/domainmodel/UserManagerDM;

    move-result-object v0

    invoke-virtual {v0}, Lcom/helpshift/account/domainmodel/UserManagerDM;->getActiveUser()Lcom/helpshift/account/domainmodel/UserDM;

    move-result-object v0

    .line 200
    iget-object v1, p0, Lcom/helpshift/JavaCore$1;->this$0:Lcom/helpshift/JavaCore;

    iget-object v1, v1, Lcom/helpshift/JavaCore;->sdkConfigurationDM:Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    iget-object v2, p0, Lcom/helpshift/JavaCore$1;->this$0:Lcom/helpshift/JavaCore;

    invoke-static {v2}, Lcom/helpshift/JavaCore;->access$000(Lcom/helpshift/JavaCore;)Lcom/helpshift/account/domainmodel/UserManagerDM;

    move-result-object v2

    invoke-virtual {v1, v2}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->fetchServerConfig(Lcom/helpshift/account/domainmodel/UserManagerDM;)Lcom/helpshift/configuration/response/RootServerConfig;

    move-result-object v1

    if-eqz v1, :cond_0

    .line 203
    new-instance v2, Lcom/helpshift/redaction/RedactionAgent;

    iget-object v3, p0, Lcom/helpshift/JavaCore$1;->this$0:Lcom/helpshift/JavaCore;

    iget-object v3, v3, Lcom/helpshift/JavaCore;->platform:Lcom/helpshift/common/platform/Platform;

    iget-object v4, p0, Lcom/helpshift/JavaCore$1;->this$0:Lcom/helpshift/JavaCore;

    invoke-static {v4}, Lcom/helpshift/JavaCore;->access$100(Lcom/helpshift/JavaCore;)Lcom/helpshift/common/domain/Domain;

    move-result-object v4

    invoke-direct {v2, v3, v4}, Lcom/helpshift/redaction/RedactionAgent;-><init>(Lcom/helpshift/common/platform/Platform;Lcom/helpshift/common/domain/Domain;)V

    .line 204
    iget-object v3, v1, Lcom/helpshift/configuration/response/RootServerConfig;->profileCreatedAt:Ljava/lang/Long;

    iget-object v1, v1, Lcom/helpshift/configuration/response/RootServerConfig;->lastRedactionAt:Ljava/lang/Long;

    invoke-virtual {v2, v0, v3, v1}, Lcom/helpshift/redaction/RedactionAgent;->checkAndUpdateRedactionState(Lcom/helpshift/account/domainmodel/UserDM;Ljava/lang/Long;Ljava/lang/Long;)V

    :cond_0
    return-void
.end method
