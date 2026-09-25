.class public Lcom/helpshift/common/platform/network/KeyValuePair;
.super Ljava/lang/Object;
.source "KeyValuePair.java"


# instance fields
.field public final key:Ljava/lang/String;

.field public final value:Ljava/lang/String;


# direct methods
.method public constructor <init>(Ljava/lang/String;Ljava/lang/String;)V
    .locals 0

    .line 7
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 8
    iput-object p1, p0, Lcom/helpshift/common/platform/network/KeyValuePair;->key:Ljava/lang/String;

    .line 9
    iput-object p2, p0, Lcom/helpshift/common/platform/network/KeyValuePair;->value:Ljava/lang/String;

    return-void
.end method
