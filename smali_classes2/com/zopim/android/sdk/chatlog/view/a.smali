.class Lcom/zopim/android/sdk/chatlog/view/a;
.super Ljava/lang/Object;

# interfaces
.implements Ljava/lang/Runnable;


# instance fields
.field final synthetic a:Landroid/graphics/drawable/AnimationDrawable;

.field final synthetic b:Lcom/zopim/android/sdk/chatlog/view/TypingIndicatorView;


# direct methods
.method constructor <init>(Lcom/zopim/android/sdk/chatlog/view/TypingIndicatorView;Landroid/graphics/drawable/AnimationDrawable;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/chatlog/view/a;->b:Lcom/zopim/android/sdk/chatlog/view/TypingIndicatorView;

    iput-object p2, p0, Lcom/zopim/android/sdk/chatlog/view/a;->a:Landroid/graphics/drawable/AnimationDrawable;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 1

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/view/a;->a:Landroid/graphics/drawable/AnimationDrawable;

    invoke-virtual {v0}, Landroid/graphics/drawable/AnimationDrawable;->start()V

    return-void
.end method
