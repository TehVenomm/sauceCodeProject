.class final Lcom/zopim/android/sdk/store/c;
.super Ljava/lang/Object;

# interfaces
.implements Lcom/zopim/android/sdk/store/VisitorInfoStorage;


# static fields
.field private static final a:Ljava/lang/String; = "c"


# direct methods
.method static constructor <clinit>()V
    .locals 0

    return-void
.end method

.method constructor <init>()V
    .locals 0

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public delete()V
    .locals 2

    sget-object v0, Lcom/zopim/android/sdk/store/c;->a:Ljava/lang/String;

    const-string v1, "Storage is not initialized. Skipping operation."

    invoke-static {v0, v1}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    return-void
.end method

.method public disable()V
    .locals 2

    sget-object v0, Lcom/zopim/android/sdk/store/c;->a:Ljava/lang/String;

    const-string v1, "Storage is not initialized. Skipping operation."

    invoke-static {v0, v1}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    return-void
.end method

.method public getVisitorInfo()Lcom/zopim/android/sdk/model/VisitorInfo;
    .locals 2

    sget-object v0, Lcom/zopim/android/sdk/store/c;->a:Ljava/lang/String;

    const-string v1, "Storage is not initialized. Skipping operation."

    invoke-static {v0, v1}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    new-instance v0, Lcom/zopim/android/sdk/model/VisitorInfo$Builder;

    invoke-direct {v0}, Lcom/zopim/android/sdk/model/VisitorInfo$Builder;-><init>()V

    invoke-virtual {v0}, Lcom/zopim/android/sdk/model/VisitorInfo$Builder;->build()Lcom/zopim/android/sdk/model/VisitorInfo;

    move-result-object v0

    return-object v0
.end method

.method public setVisitorInfo(Lcom/zopim/android/sdk/model/VisitorInfo;)V
    .locals 1

    sget-object p1, Lcom/zopim/android/sdk/store/c;->a:Ljava/lang/String;

    const-string v0, "Storage is not initialized. Skipping operation."

    invoke-static {p1, v0}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    return-void
.end method
