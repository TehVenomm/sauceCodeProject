.class Lcom/helpshift/account/domainmodel/UserManagerDM$1;
.super Lcom/helpshift/common/domain/F;
.source "UserManagerDM.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/account/domainmodel/UserManagerDM;->sendPushToken()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/account/domainmodel/UserManagerDM;


# direct methods
.method constructor <init>(Lcom/helpshift/account/domainmodel/UserManagerDM;)V
    .locals 0

    .line 304
    iput-object p1, p0, Lcom/helpshift/account/domainmodel/UserManagerDM$1;->this$0:Lcom/helpshift/account/domainmodel/UserManagerDM;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 1

    .line 307
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserManagerDM$1;->this$0:Lcom/helpshift/account/domainmodel/UserManagerDM;

    invoke-virtual {v0}, Lcom/helpshift/account/domainmodel/UserManagerDM;->sendPushTokenSync()V

    return-void
.end method
