.class public final Lcom/zopim/android/sdk/embeddable/Contract;
.super Ljava/lang/Object;


# static fields
.field public static final ACTION_CREATE_REQUEST:Ljava/lang/String; = "zopim.action.CREATE_REQUEST"


# direct methods
.method public constructor <init>()V
    .locals 0

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static getChatSdkVersionName()Ljava/lang/String;
    .locals 1

    const-string v0, "1.0.1"

    return-object v0
.end method
