.class Lcom/helpshift/common/AutoRetryFailedEventDM$2;
.super Lcom/helpshift/common/domain/F;
.source "AutoRetryFailedEventDM.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/common/AutoRetryFailedEventDM;->sendEventForcefully(Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/common/AutoRetryFailedEventDM;

.field final synthetic val$eventType:Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;


# direct methods
.method constructor <init>(Lcom/helpshift/common/AutoRetryFailedEventDM;Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;)V
    .locals 0

    .line 95
    iput-object p1, p0, Lcom/helpshift/common/AutoRetryFailedEventDM$2;->this$0:Lcom/helpshift/common/AutoRetryFailedEventDM;

    iput-object p2, p0, Lcom/helpshift/common/AutoRetryFailedEventDM$2;->val$eventType:Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 2

    .line 98
    new-instance v0, Ljava/util/LinkedHashSet;

    invoke-direct {v0}, Ljava/util/LinkedHashSet;-><init>()V

    .line 99
    iget-object v1, p0, Lcom/helpshift/common/AutoRetryFailedEventDM$2;->val$eventType:Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;

    invoke-interface {v0, v1}, Ljava/util/Set;->add(Ljava/lang/Object;)Z

    .line 100
    iget-object v1, p0, Lcom/helpshift/common/AutoRetryFailedEventDM$2;->this$0:Lcom/helpshift/common/AutoRetryFailedEventDM;

    invoke-virtual {v1, v0}, Lcom/helpshift/common/AutoRetryFailedEventDM;->retryFailedApis(Ljava/util/Set;)V

    return-void
.end method
