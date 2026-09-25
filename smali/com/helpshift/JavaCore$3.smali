.class Lcom/helpshift/JavaCore$3;
.super Lcom/helpshift/common/domain/F;
.source "JavaCore.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/JavaCore;->sendAppStartEvent()V
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

    .line 253
    iput-object p1, p0, Lcom/helpshift/JavaCore$3;->this$0:Lcom/helpshift/JavaCore;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 2

    .line 257
    iget-object v0, p0, Lcom/helpshift/JavaCore$3;->this$0:Lcom/helpshift/JavaCore;

    iget-object v0, v0, Lcom/helpshift/JavaCore;->analyticsEventDM:Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;

    iget-object v1, p0, Lcom/helpshift/JavaCore$3;->this$0:Lcom/helpshift/JavaCore;

    invoke-virtual {v1}, Lcom/helpshift/JavaCore;->getUserManagerDM()Lcom/helpshift/account/domainmodel/UserManagerDM;

    move-result-object v1

    invoke-virtual {v1}, Lcom/helpshift/account/domainmodel/UserManagerDM;->getActiveUser()Lcom/helpshift/account/domainmodel/UserDM;

    move-result-object v1

    invoke-virtual {v0, v1}, Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;->sendAppStartEvent(Lcom/helpshift/account/domainmodel/UserDM;)V

    return-void
.end method
