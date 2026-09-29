.class public Lcom/helpshift/logger/model/LogModel;
.super Ljava/lang/Object;
.source "LogModel.java"


# instance fields
.field public final extras:Ljava/lang/String;

.field public final level:Ljava/lang/String;

.field public final message:Ljava/lang/String;

.field public final sdkVersion:Ljava/lang/String;

.field public final stacktrace:Ljava/lang/String;

.field public final timeStamp:Ljava/lang/String;


# direct methods
.method public constructor <init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    .locals 0

    .line 49
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 50
    iput-object p1, p0, Lcom/helpshift/logger/model/LogModel;->timeStamp:Ljava/lang/String;

    .line 51
    iput-object p5, p0, Lcom/helpshift/logger/model/LogModel;->extras:Ljava/lang/String;

    .line 52
    iput-object p2, p0, Lcom/helpshift/logger/model/LogModel;->level:Ljava/lang/String;

    .line 53
    iput-object p4, p0, Lcom/helpshift/logger/model/LogModel;->stacktrace:Ljava/lang/String;

    .line 54
    iput-object p3, p0, Lcom/helpshift/logger/model/LogModel;->message:Ljava/lang/String;

    .line 55
    iput-object p6, p0, Lcom/helpshift/logger/model/LogModel;->sdkVersion:Ljava/lang/String;

    return-void
.end method
