.class Lcom/zopim/android/sdk/attachment/ImagePicker$b;
.super Ljava/lang/Object;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/zopim/android/sdk/attachment/ImagePicker;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = "b"
.end annotation


# instance fields
.field final synthetic a:Lcom/zopim/android/sdk/attachment/ImagePicker;

.field private final b:Z

.field private final c:Landroid/net/Uri;

.field private final d:Ljava/io/File;


# direct methods
.method constructor <init>(Lcom/zopim/android/sdk/attachment/ImagePicker;Landroid/net/Uri;Ljava/io/File;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/attachment/ImagePicker$b;->a:Lcom/zopim/android/sdk/attachment/ImagePicker;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    iput-object p2, p0, Lcom/zopim/android/sdk/attachment/ImagePicker$b;->c:Landroid/net/Uri;

    iput-object p3, p0, Lcom/zopim/android/sdk/attachment/ImagePicker$b;->d:Ljava/io/File;

    if-eqz p3, :cond_0

    const/4 p1, 0x1

    goto :goto_0

    :cond_0
    const/4 p1, 0x0

    :goto_0
    iput-boolean p1, p0, Lcom/zopim/android/sdk/attachment/ImagePicker$b;->b:Z

    return-void
.end method


# virtual methods
.method public a()Z
    .locals 1

    iget-boolean v0, p0, Lcom/zopim/android/sdk/attachment/ImagePicker$b;->b:Z

    return v0
.end method

.method public b()Landroid/net/Uri;
    .locals 1

    iget-object v0, p0, Lcom/zopim/android/sdk/attachment/ImagePicker$b;->c:Landroid/net/Uri;

    return-object v0
.end method

.method public c()Ljava/io/File;
    .locals 1
    .annotation build Landroidx/annotation/Nullable;
    .end annotation

    iget-object v0, p0, Lcom/zopim/android/sdk/attachment/ImagePicker$b;->d:Ljava/io/File;

    return-object v0
.end method
