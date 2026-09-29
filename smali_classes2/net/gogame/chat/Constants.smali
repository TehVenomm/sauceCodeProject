.class public Lnet/gogame/chat/Constants;
.super Ljava/lang/Object;
.source "Constants.java"


# static fields
.field public static final FRAGMENT_CONTAINER:I

.field public static final MAX_IMAGE_SIZE:J = 0x380000L

.field public static final TAG:Ljava/lang/String; = "zopim-client"


# direct methods
.method static constructor <clinit>()V
    .locals 1

    .line 8
    sget v0, Lcom/zopim/android/sdk/R$id;->fragment:I

    sput v0, Lnet/gogame/chat/Constants;->FRAGMENT_CONTAINER:I

    return-void
.end method

.method public constructor <init>()V
    .locals 0

    .line 5
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method
