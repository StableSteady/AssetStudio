using AssetStudio;

class Program
{
    static void Main(string[] args)
    {
        string path = args[0];

        loadFile(path);
        exportAnimatorwithAnimationClip(path);
    }

    static void loadFile(string file)
    {
        assetsManager.SpecifyUnityVersion = "";
        assetsManager.LoadFiles(file);
    }

    static void exportAnimatorwithAnimationClip(string path)
    {
        Animator animator = null;
        List<AnimationClip> animationList = new List<AnimationClip>();
        foreach (var assetsFile in assetsManager.assetsFileList)
        {
            foreach (var obj in assetsFile.Objects)
            {
                switch (obj)
                {
                    case Animator m_Animator:
                        if (m_Animator.m_GameObject.TryGet(out var gameObject))
                        {
                            if (gameObject.m_Name == "chr")
                            {
                                animator = m_Animator;
                            }
                        }
                        break;
                    case AnimationClip animationClip:
                        animationList.Add(animationClip);
                        break;
                }
            }
        }
        ExportAnimatorWithAnimationClip(animator, animationList, Path.Combine(Directory.GetParent(path).ToString(), "chr.fbx"));
    }

    static void ExportAnimatorWithAnimationClip(Animator animator, List<AnimationClip> animationList, string exportPath)
    {

        var convert = new ModelConverter(animator, ImageFormat.Png, animationList.ToArray());
        ModelExporter.ExportFbx(exportPath, convert, true, (float)0.25, true, true, true, true, false, 10, false, 1, 3, false);
    }

    public static AssetsManager assetsManager = new AssetsManager();
}
