.class Lcom/zopim/android/sdk/attachment/a;
.super Ljava/util/ArrayList;


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/util/ArrayList<",
        "Ljava/io/File;",
        ">;"
    }
.end annotation


# instance fields
.field final synthetic a:Ljava/io/File;

.field final synthetic b:Lcom/zopim/android/sdk/attachment/ImagePicker;


# direct methods
.method constructor <init>(Lcom/zopim/android/sdk/attachment/ImagePicker;Ljava/io/File;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/attachment/a;->b:Lcom/zopim/android/sdk/attachment/ImagePicker;

    iput-object p2, p0, Lcom/zopim/android/sdk/attachment/a;->a:Ljava/io/File;

    invoke-direct {p0}, Ljava/util/ArrayList;-><init>()V

    iget-object p1, p0, Lcom/zopim/android/sdk/attachment/a;->a:Ljava/io/File;

    invoke-virtual {p0, p1}, Lcom/zopim/android/sdk/attachment/a;->add(Ljava/lang/Object;)Z

    return-void
.end method
