.class public Lcom/helpshift/meta/dto/DebugLogDTO;
.super Ljava/lang/Object;
.source "DebugLogDTO.java"


# instance fields
.field public final level:Ljava/lang/Integer;

.field public final msg:Ljava/lang/String;

.field public final tag:Ljava/lang/String;

.field public final throwable:Ljava/lang/String;


# direct methods
.method public constructor <init>(Ljava/lang/Integer;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    .locals 0

    .line 10
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 11
    iput-object p1, p0, Lcom/helpshift/meta/dto/DebugLogDTO;->level:Ljava/lang/Integer;

    .line 12
    iput-object p2, p0, Lcom/helpshift/meta/dto/DebugLogDTO;->tag:Ljava/lang/String;

    .line 13
    iput-object p3, p0, Lcom/helpshift/meta/dto/DebugLogDTO;->msg:Ljava/lang/String;

    .line 14
    iput-object p4, p0, Lcom/helpshift/meta/dto/DebugLogDTO;->throwable:Ljava/lang/String;

    return-void
.end method
