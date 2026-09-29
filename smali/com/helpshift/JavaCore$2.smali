.class Lcom/helpshift/JavaCore$2;
.super Lcom/helpshift/common/domain/F;
.source "JavaCore.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/JavaCore;->sendAnalyticsEvent()V
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

    .line 233
    iput-object p1, p0, Lcom/helpshift/JavaCore$2;->this$0:Lcom/helpshift/JavaCore;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 2

    .line 236
    iget-object v0, p0, Lcom/helpshift/JavaCore$2;->this$0:Lcom/helpshift/JavaCore;

    iget-object v0, v0, Lcom/helpshift/JavaCore;->analyticsEventDM:Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;

    if-eqz v0, :cond_0

    .line 240
    iget-object v0, p0, Lcom/helpshift/JavaCore$2;->this$0:Lcom/helpshift/JavaCore;

    iget-object v0, v0, Lcom/helpshift/JavaCore;->analyticsEventDM:Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;

    iget-object v1, p0, Lcom/helpshift/JavaCore$2;->this$0:Lcom/helpshift/JavaCore;

    invoke-virtual {v1}, Lcom/helpshift/JavaCore;->getUserManagerDM()Lcom/helpshift/account/domainmodel/UserManagerDM;

    move-result-object v1

    invoke-virtual {v1}, Lcom/helpshift/account/domainmodel/UserManagerDM;->getActiveUser()Lcom/helpshift/account/domainmodel/UserDM;

    move-result-object v1

    invoke-virtual {v0, v1}, Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;->sendEventsToServer(Lcom/helpshift/account/domainmodel/UserDM;)V

    :cond_0
    return-void
.end method
