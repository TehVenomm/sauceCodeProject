.class public Lcom/helpshift/network/request/RequestQueue$DeliveryType;
.super Ljava/lang/Object;
.source "RequestQueue.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/network/request/RequestQueue;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x9
    name = "DeliveryType"
.end annotation


# static fields
.field public static final ON_NEW_THREAD:Ljava/lang/Integer;


# direct methods
.method static constructor <clinit>()V
    .locals 1

    const/4 v0, 0x1

    .line 97
    invoke-static {v0}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v0

    sput-object v0, Lcom/helpshift/network/request/RequestQueue$DeliveryType;->ON_NEW_THREAD:Ljava/lang/Integer;

    return-void
.end method

.method public constructor <init>()V
    .locals 0

    .line 96
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method
